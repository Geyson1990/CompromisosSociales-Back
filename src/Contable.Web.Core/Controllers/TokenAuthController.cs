using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.AspNetZeroCore.Web.Authentication.External;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Abp.Configuration;
using Abp.Extensions;
using Abp.Net.Mail;
using Abp.Notifications;
using Abp.Runtime.Caching;
using Abp.Runtime.Security;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.UI;
using Abp.Zero.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Contable.Authentication.TwoFactor.Google;
using Contable.Authorization;
using Contable.Authorization.Accounts.Dto;
using Contable.Authorization.Users;
using Contable.MultiTenancy;
using Contable.Web.Authentication.JwtBearer;
using Contable.Web.Authentication.TwoFactor;
using Contable.Web.Models.TokenAuth;
using Contable.Authorization.Impersonation;
using Contable.Authorization.Roles;
using Contable.Configuration;
using Contable.Debugging;
using Contable.Identity;
using Contable.Net.Sms;
using Contable.Notifications;
using Contable.Security.Recaptcha;
using Contable.Web.Authentication.External;
using Contable.Web.Common;
using Contable.Authorization.Delegation;
using System.Security.Cryptography;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Contable.Application.Extensions;
using Contable.Authorization.Users.Profile.Cache;

namespace Contable.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TokenAuthController : ContableControllerBase
    {
        private const string UserIdentifierClaimType = "http://aspnetzero.com/claims/useridentifier";

        private readonly LogInManager _logInManager;
        private readonly ITenantCache _tenantCache;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly TokenAuthConfiguration _configuration;
        private readonly UserManager _userManager;
        private readonly ICacheManager _cacheManager;
        private readonly IOptions<JwtBearerOptions> _jwtOptions;
        private readonly IExternalAuthConfiguration _externalAuthConfiguration;
        private readonly IExternalAuthManager _externalAuthManager;
        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly IImpersonationManager _impersonationManager;
        private readonly IUserLinkManager _userLinkManager;
        private readonly ISmsSender _smsSender;
        private readonly IEmailSender _emailSender;
        private readonly IdentityOptions _identityOptions;
        private readonly GoogleAuthenticatorProvider _googleAuthenticatorProvider;
        private readonly ExternalLoginInfoManagerFactory _externalLoginInfoManagerFactory;
        private readonly ISettingManager _settingManager;
        private readonly IJwtSecurityStampHandler _securityStampHandler;
        private readonly AbpUserClaimsPrincipalFactory<User, Role> _claimsPrincipalFactory;
        private readonly IConfigurationRoot _configurationRoot;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IRecaptchaValidator RecaptchaValidator { get; set; }
        private readonly IUserDelegationManager _userDelegationManager;

        public TokenAuthController(
            LogInManager logInManager,
            ITenantCache tenantCache,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            TokenAuthConfiguration configuration,
            UserManager userManager,
            ICacheManager cacheManager,
            IOptions<JwtBearerOptions> jwtOptions,
            IExternalAuthConfiguration externalAuthConfiguration,
            IExternalAuthManager externalAuthManager,
            UserRegistrationManager userRegistrationManager,
            IImpersonationManager impersonationManager,
            IUserLinkManager userLinkManager,
            ISmsSender smsSender,
            IEmailSender emailSender,
            IOptions<IdentityOptions> identityOptions,
            GoogleAuthenticatorProvider googleAuthenticatorProvider,
            ExternalLoginInfoManagerFactory externalLoginInfoManagerFactory,
            ISettingManager settingManager,
            IJwtSecurityStampHandler securityStampHandler,
            AbpUserClaimsPrincipalFactory<User, Role> claimsPrincipalFactory, 
            IUserDelegationManager userDelegationManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _logInManager = logInManager;
            _tenantCache = tenantCache;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _configuration = configuration;
            _userManager = userManager;
            _cacheManager = cacheManager;
            _jwtOptions = jwtOptions;
            _externalAuthConfiguration = externalAuthConfiguration;
            _externalAuthManager = externalAuthManager;
            _userRegistrationManager = userRegistrationManager;
            _impersonationManager = impersonationManager;
            _userLinkManager = userLinkManager;
            _smsSender = smsSender;
            _emailSender = emailSender;
            _googleAuthenticatorProvider = googleAuthenticatorProvider;
            _externalLoginInfoManagerFactory = externalLoginInfoManagerFactory;
            _settingManager = settingManager;
            _securityStampHandler = securityStampHandler;
            _identityOptions = identityOptions.Value;
            _claimsPrincipalFactory = claimsPrincipalFactory;
            RecaptchaValidator = NullRecaptchaValidator.Instance;
            _userDelegationManager = userDelegationManager;
            _webHostEnvironment = webHostEnvironment;
            _configurationRoot = webHostEnvironment.GetAppConfiguration();
        }

        [HttpPost]
        public async Task<AuthenticateResultModel> Authenticate([FromBody] AuthenticateModel model)
        {
            await ValidateCaptcha(model.CaptchaSecurityCode, model.CaptchaKey);

            return await CompleteAuth(model);
        }

        [HttpPost]
        public async Task<AuthenticateResultModel> ExternalAuth([FromBody] ExternalAuthenticateModel model)
        {
            return await CompleteAuth(new AuthenticateModel()
            {
                UserNameOrEmailAddress = model.UserNameOrEmailAddress,
                Password = model.Password
            });
        }

        [HttpPost]
        public async Task<RefreshTokenResult> RefreshToken(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                throw new ArgumentNullException(nameof(refreshToken));
            }

            if (!IsRefreshTokenValid(refreshToken, out var principal))
            {
                throw new ValidationException("Refresh token is not valid!");
            }

            try
            {
                var user = _userManager.GetUser(UserIdentifier.Parse(principal.Claims.First(x => x.Type == AppConsts.UserIdentifier).Value));

                if (user == null)
                {
                    throw new UserFriendlyException("Unknown user or user identifier");
                }

                principal = await _claimsPrincipalFactory.CreateAsync(user);

                var accessToken = CreateAccessToken(await CreateJwtClaims(principal.Identity as ClaimsIdentity, user));

                return await Task.FromResult(new RefreshTokenResult(accessToken, GetEncryptedAccessToken(accessToken), (int)_configuration.AccessTokenExpiration.TotalSeconds));
            }
            catch (UserFriendlyException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ValidationException("Refresh token is not valid!", e);
            }
        }

        [HttpGet]
        [AbpAuthorize]
        public async Task LogOut()
        {
            if (AbpSession.UserId != null)
            {
                var tokenValidityKeyInClaims = User.Claims.First(c => c.Type == AppConsts.TokenValidityKey);
                await _userManager.RemoveTokenValidityKeyAsync(_userManager.GetUser(AbpSession.ToUserIdentifier()), tokenValidityKeyInClaims.Value);
                _cacheManager.GetCache(AppConsts.TokenValidityKey).Remove(tokenValidityKeyInClaims.Value);

                if (AllowOneConcurrentLoginPerUser())
                {
                    await _securityStampHandler.RemoveSecurityStampCacheItem(AbpSession.TenantId, AbpSession.GetUserId());
                }
            }
        }

        private async Task<AuthenticateResultModel> CompleteAuth(AuthenticateModel model)
        {

            var key = _configurationRoot["Authentication:AES:Key"];
            var iv = _configurationRoot["Authentication:AES:IV"];

            try
            {
                model.UserNameOrEmailAddress = HelperExtensions.DecryptStringAES(cipherText: model.UserNameOrEmailAddress, keyText: key, ivText: iv);
                model.Password = HelperExtensions.DecryptStringAES(cipherText: model.Password, keyText: key, ivText: iv);
            }
            catch
            {
                throw new UserFriendlyException(L("InvalidUserNameOrPassword"));
            }

            var loginResult = await GetLoginResultAsync(
                model.UserNameOrEmailAddress,
                model.Password,
                GetTenancyNameOrNull());

            var returnUrl = model.ReturnUrl;

            if (model.SingleSignIn.HasValue && model.SingleSignIn.Value && loginResult.Result == AbpLoginResultType.Success)
            {
                loginResult.User.SetSignInToken();
                returnUrl = AddSingleSignInParametersToReturnUrl(model.ReturnUrl, loginResult.User.SignInToken, loginResult.User.Id, loginResult.User.TenantId);
            }

            //Password reset
            if (loginResult.User.ShouldChangePasswordOnNextLogin)
            {
                loginResult.User.SetNewPasswordResetCode();
                return new AuthenticateResultModel
                {
                    ShouldResetPassword = true,
                    PasswordResetCode = loginResult.User.PasswordResetCode,
                    UserId = loginResult.User.Id,
                    ReturnUrl = returnUrl
                };
            }

            // One Concurrent Login 
            if (AllowOneConcurrentLoginPerUser())
            {
                await _userManager.UpdateSecurityStampAsync(loginResult.User);
                await _securityStampHandler.SetSecurityStampCacheItem(loginResult.User.TenantId, loginResult.User.Id, loginResult.User.SecurityStamp);
                loginResult.Identity.ReplaceClaim(new Claim(AppConsts.SecurityStampKey, loginResult.User.SecurityStamp));
            }

            var accessToken = CreateAccessToken(await CreateJwtClaims(loginResult.Identity, loginResult.User));
            var refreshToken = CreateRefreshToken(await CreateJwtClaims(loginResult.Identity, loginResult.User, tokenType: TokenType.RefreshToken));

            return new AuthenticateResultModel
            {
                AccessToken = accessToken,
                ExpireInSeconds = (int)_configuration.AccessTokenExpiration.TotalSeconds,
                RefreshToken = refreshToken,
                RefreshTokenExpireInSeconds = (int)_configuration.RefreshTokenExpiration.TotalSeconds,
                EncryptedAccessToken = GetEncryptedAccessToken(accessToken),
                UserId = loginResult.User.Id,
                ReturnUrl = returnUrl
            };
        }

        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            return CreateToken(claims, expiration ?? _configuration.AccessTokenExpiration);
        }

        private string CreateRefreshToken(IEnumerable<Claim> claims)
        {
            return CreateToken(claims, AppConsts.RefreshTokenExpiration);
        }

        private string CreateToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                notBefore: now,
                signingCredentials: _configuration.SigningCredentials,
                expires: expiration == null ?
                    (DateTime?)null :
                    now.Add(expiration.Value)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private static string GetEncryptedAccessToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken, AppConsts.DefaultPassPhrase);
        }

        private async Task<IEnumerable<Claim>> CreateJwtClaims(ClaimsIdentity identity, User user, TimeSpan? expiration = null, TokenType tokenType = TokenType.AccessToken)
        {
            var tokenValidityKey = Guid.NewGuid().ToString();
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == _identityOptions.ClaimsIdentity.UserIdClaimType);

            if (!expiration.HasValue)
            {
                expiration = tokenType == TokenType.AccessToken
                    ? _configuration.AccessTokenExpiration
                    : _configuration.RefreshTokenExpiration;
            }

            if (_identityOptions.ClaimsIdentity.UserIdClaimType != JwtRegisteredClaimNames.Sub)
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value));
            }

            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(AppConsts.TokenValidityKey, tokenValidityKey),
                new Claim(AppConsts.UserIdentifier, user.ToUserIdentifier().ToUserIdentifierString()),
                new Claim(AppConsts.TokenType, tokenType.To<int>().ToString()),
                new Claim(AppConsts.TokenExpiration, expiration.Value.TotalSeconds.ToString())
             });

            _cacheManager
                .GetCache(AppConsts.TokenValidityKey)
                .Set(tokenValidityKey, "", absoluteExpireTime: expiration);

            await _userManager.AddTokenValidityKeyAsync(
                user,
                tokenValidityKey,
                DateTime.UtcNow.Add(expiration.Value)
            );

            return claims;
        }

        private static string AddSingleSignInParametersToReturnUrl(string returnUrl, string signInToken, long userId, int? tenantId)
        {
            returnUrl += (returnUrl.Contains("?") ? "&" : "?") +
                         "accessToken=" + signInToken +
                         "&userId=" + Convert.ToBase64String(Encoding.UTF8.GetBytes(userId.ToString()));
            if (tenantId.HasValue)
            {
                returnUrl += "&tenantId=" + Convert.ToBase64String(Encoding.UTF8.GetBytes(tenantId.Value.ToString()));
            }

            return returnUrl;
        }


        private bool IsRefreshTokenValid(string refreshToken, out ClaimsPrincipal principal)
        {
            principal = null;

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidAudience = _configuration.Audience,
                    ValidIssuer = _configuration.Issuer,
                    IssuerSigningKey = _configuration.SecurityKey
                };

                foreach (var validator in _jwtOptions.Value.SecurityTokenValidators)
                {
                    if (!validator.CanReadToken(refreshToken))
                    {
                        continue;
                    }

                    try
                    {
                        principal = validator.ValidateToken(refreshToken, validationParameters, out _);

                        if (principal.Claims.FirstOrDefault(x => x.Type == AppConsts.TokenType)?.Value == TokenType.RefreshToken.To<int>().ToString())
                        {
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Debug(ex.ToString(), ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Debug(ex.ToString(), ex);
            }

            return false;
        }

        private bool AllowOneConcurrentLoginPerUser()
        {
            return _settingManager.GetSettingValue<bool>(AppSettings.UserManagement.AllowOneConcurrentLoginPerUser);
        }

        private async Task ValidateCaptcha(string captchaSecurityCode, string captchaKey)
        {
            captchaKey = (captchaKey ?? string.Empty).Trim().ToUpper();

            var cacheItem = await _cacheManager
                .GetLoginCaptchaCodeCache()
                .GetOrDefaultAsync(captchaSecurityCode);

            if (cacheItem == null)
                throw new UserFriendlyException(1500, "Aviso", "El código captcha ha exirado, por favor ingrese el código nuevamente.");

            if (!cacheItem.Code.Equals(captchaKey))
            {
                await _cacheManager
                    .GetLoginCaptchaCodeCache()
                    .RemoveAsync(captchaSecurityCode);

                throw new UserFriendlyException(1500, "Aviso", "El código captcha ingresado es inválido, por favor ingrese el código nuevamente.");
            }

            await _cacheManager
                .GetLoginCaptchaCodeCache()
                .RemoveAsync(captchaSecurityCode);
        }
    }
}
