using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Abp;
using Abp.Auditing;
using Abp.Authorization;
using Abp.BackgroundJobs;
using Abp.Configuration;
using Abp.Extensions;
using Abp.Localization;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.UI;
using Abp.Zero.Configuration;
using Microsoft.AspNetCore.Identity;
using Contable.Authentication.TwoFactor.Google;
using Contable.Authorization.Users.Dto;
using Contable.Authorization.Users.Profile.Cache;
using Contable.Authorization.Users.Profile.Dto;
using Contable.Configuration;
using Contable.Friendships;
using Contable.Gdpr;
using Contable.Net.Sms;
using Contable.Security;
using Contable.Storage;
using Contable.Timing;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Hosting;
using Contable.Application.Extensions;
using Contable.Application.Uploaders.Dto;
using Abp.Domain.Repositories;
using Contable.Application;
using Microsoft.Extensions.Configuration;

namespace Contable.Authorization.Users.Profile
{
    [AbpAuthorize]
    public class ProfileAppService : ContableAppServiceBase, IProfileAppService
    {
        private const int MaxProfilPictureBytes = 5242880; //5MB
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ITimeZoneService _timeZoneService;
        private readonly IFriendshipManager _friendshipManager;
        private readonly GoogleTwoFactorAuthenticateService _googleTwoFactorAuthenticateService;
        private readonly ISmsSender _smsSender;
        private readonly ICacheManager _cacheManager;
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly ProfileImageServiceFactory _profileImageServiceFactory;
        private readonly IRepository<User, long> _userRepository;
        private readonly IConfigurationRoot _configurationRoot;

        public ProfileAppService(
            IAppFolders appFolders,
            IBinaryObjectManager binaryObjectManager,
            ITimeZoneService timezoneService,
            IFriendshipManager friendshipManager,
            GoogleTwoFactorAuthenticateService googleTwoFactorAuthenticateService,
            ISmsSender smsSender,
            ICacheManager cacheManager,
            ITempFileCacheManager tempFileCacheManager,
            IBackgroundJobManager backgroundJobManager,
            ProfileImageServiceFactory profileImageServiceFactory,
            IRepository<User, long> userRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _binaryObjectManager = binaryObjectManager;
            _timeZoneService = timezoneService;
            _friendshipManager = friendshipManager;
            _googleTwoFactorAuthenticateService = googleTwoFactorAuthenticateService;
            _smsSender = smsSender;
            _cacheManager = cacheManager;
            _tempFileCacheManager = tempFileCacheManager;
            _backgroundJobManager = backgroundJobManager;
            _profileImageServiceFactory = profileImageServiceFactory;
            _userRepository = userRepository;
            _configurationRoot = webHostEnvironment.GetAppConfiguration();
        }

        [DisableAuditing]
        public async Task<CurrentUserProfileEditDto> GetCurrentUserProfileForEdit()
        {
            return ObjectMapper.Map<CurrentUserProfileEditDto>(await GetCurrentUserAsync());
        }

        public async Task UpdateCurrentUserProfile(CurrentUserProfileEditDto input)
        {
            var user = await GetCurrentUserAsync();

            input.Document.VerifyTableColumn(UserConsts.DocumentMinLength,
                UserConsts.DocumentMaxLength,
                "Aviso", $"El número de DNI no debe exceder los {UserConsts.DocumentMaxLength} caracteres");

            input.Name.IsValidOrException("Aviso", "El nombre es obligatorio");
            input.Name.VerifyTableColumn(UserConsts.NameMinLength,
                UserConsts.NameMaxLength,
                "Aviso", $"El nombre no debe exceder los {UserConsts.NameMaxLength} caracteres");

            input.Surname.IsValidOrException("Aviso", "El apellido paterno es obligatorio");
            input.Surname.VerifyTableColumn(UserConsts.SurnameMinLength,
                UserConsts.SurnameMaxLength,
                "Aviso", $"El apellido paterno no debe exceder los {UserConsts.SurnameMaxLength} caracteres");

            input.Surname2.IsValidOrException("Aviso", "El apellido materno es obligatorio");
            input.Surname2.VerifyTableColumn(UserConsts.Surname2MinLength,
                UserConsts.Surname2MaxLength,
                "Aviso", $"El apellido materno no debe exceder los {UserConsts.Surname2MaxLength} caracteres");

            input.Document = input.Document == null ? null : input.Document.Trim();
            input.Name = input.Name == null ? null : input.Name.ToUpper().Trim();
            input.Surname = input.Surname == null ? null : input.Surname.ToUpper().Trim();
            input.Surname2 = input.Surname2 == null ? null : input.Surname2.ToUpper().Trim();

            ObjectMapper.Map(input, user);

            CheckErrors(await UserManager.UpdateAsync(user));
        }

        public async Task ChangePassword(ChangePasswordInput input)
        {
            if (string.IsNullOrWhiteSpace(input.CurrentPassword))
                throw new UserFriendlyException("Aviso", "La contraseña actual es obligatoria");
            if (string.IsNullOrWhiteSpace(input.NewPassword))
                throw new UserFriendlyException("Aviso", "La contraseña nueva es obligatoria");

            var key = _configurationRoot["Authentication:AES:Key"];
            var iv = _configurationRoot["Authentication:AES:IV"];

            try
            {
                input.CurrentPassword = HelperExtensions.DecryptStringAES(cipherText: input.CurrentPassword, keyText: key, ivText: iv);
                input.NewPassword = HelperExtensions.DecryptStringAES(cipherText: input.NewPassword, keyText: key, ivText: iv);
            }
            catch
            {
                throw new UserFriendlyException("Aviso", "La contraseña ingresada es inválida");
            }

            await UserManager.InitializeOptionsAsync(AbpSession.TenantId);

            var user = await GetCurrentUserAsync();

            if (await UserManager.CheckPasswordAsync(user, input.CurrentPassword))
            {
                CheckErrors(await UserManager.ChangePasswordAsync(user, input.NewPassword));
            }
            else
            {
                CheckErrors(IdentityResult.Failed(new IdentityError
                {
                    Description = "Incorrect password."
                }));
            }
        }

        public async Task<UpdateProfilePictureOutput> UpdateProfilePicture(UpdateProfilePictureInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Token))
                throw new UserFriendlyException("Aviso", "No se puede procesar la solicitud actual. Por favor intente nuevamente más tarde");

            var user = await GetCurrentUserAsync();

            var upload = ResourceManager.Create(resource: new UploadResourceInputDto()
            {
                Token = input.Token,
                Extension = input.Extension,
                FileName = $"{user.Id}"
            }, section: ResourceConsts.ProfilePicture, replaceName: true);

            user.ProfilePicture = upload.FileName;

            await _userRepository.UpdateAsync(user);

            return new UpdateProfilePictureOutput()
            {
                Resource = user.ProfilePicture
            };
        }

        [AbpAllowAnonymous]
        public async Task<GetPasswordComplexitySettingOutput> GetPasswordComplexitySetting()
        {
            var passwordComplexitySetting = new PasswordComplexitySetting
            {
                RequireDigit =
                    await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement
                        .PasswordComplexity.RequireDigit),
                RequireLowercase =
                    await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement
                        .PasswordComplexity.RequireLowercase),
                RequireNonAlphanumeric =
                    await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement
                        .PasswordComplexity.RequireNonAlphanumeric),
                RequireUppercase =
                    await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement
                        .PasswordComplexity.RequireUppercase),
                RequiredLength =
                    await SettingManager.GetSettingValueAsync<int>(AbpZeroSettingNames.UserManagement.PasswordComplexity
                        .RequiredLength)
            };

            return new GetPasswordComplexitySettingOutput
            {
                Setting = passwordComplexitySetting
            };
        }

        [DisableAuditing]
        public async Task<GetProfilePictureOutput> GetProfilePicture()
        {
            using (var profileImageService = await _profileImageServiceFactory.Get(AbpSession.ToUserIdentifier()))
            {
                var profilePictureContent = await profileImageService.Object.GetProfilePictureContentForUser(
                    AbpSession.ToUserIdentifier()
                );
                
                return new GetProfilePictureOutput(profilePictureContent);
            }
        }

        [AbpAllowAnonymous]
        public async Task<GetProfilePictureOutput> GetProfilePictureByUserName(string username)
        {
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                return new GetProfilePictureOutput(string.Empty);
            }
            
            var userIdentifier = new UserIdentifier(AbpSession.TenantId, user.Id);
            using (var profileImageService = await _profileImageServiceFactory.Get(userIdentifier))
            {
                var profileImage = await profileImageService.Object.GetProfilePictureContentForUser(userIdentifier);
                return new GetProfilePictureOutput(profileImage);
            }
        }

        public async Task<GetProfilePictureOutput> GetFriendProfilePicture(GetFriendProfilePictureInput input)
        {
            var friendUserIdentifier = input.ToUserIdentifier();
            var friendShip = await _friendshipManager.GetFriendshipOrNullAsync(
                AbpSession.ToUserIdentifier(),
                friendUserIdentifier
            );

            if (friendShip == null)
            {
                return new GetProfilePictureOutput(string.Empty);
            }

            
            using (var profileImageService = await _profileImageServiceFactory.Get(friendUserIdentifier))
            {
                var image = await profileImageService.Object.GetProfilePictureContentForUser(friendUserIdentifier);
                return new GetProfilePictureOutput(image);
            }
        }

        [AbpAllowAnonymous]
        public async Task<GetProfilePictureOutput> GetProfilePictureByUser(long userId)
        {
            var userIdentifier = new UserIdentifier(AbpSession.TenantId, userId);
            using (var profileImageService = await _profileImageServiceFactory.Get(userIdentifier))
            {
                var profileImage = await profileImageService.Object.GetProfilePictureContentForUser(userIdentifier);
                return new GetProfilePictureOutput(profileImage);
            }
        }

        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        private async Task<byte[]> GetProfilePictureByIdOrNull(Guid profilePictureId)
        {
            var file = await _binaryObjectManager.GetOrNullAsync(profilePictureId);
            if (file == null)
            {
                return null;
            }

            return file.Bytes;
        }

        private async Task<GetProfilePictureOutput> GetProfilePictureByIdInternal(Guid profilePictureId)
        {
            var bytes = await GetProfilePictureByIdOrNull(profilePictureId);
            if (bytes == null)
            {
                return new GetProfilePictureOutput(string.Empty);
            }

            return new GetProfilePictureOutput(Convert.ToBase64String(bytes));
        }

       
    }
}