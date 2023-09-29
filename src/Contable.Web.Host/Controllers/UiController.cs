using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration.Startup;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Contable.Authorization;
using Contable.Authorization.Accounts;
using Contable.Authorization.Accounts.Dto;
using Contable.Authorization.Users;
using Contable.Identity;
using Contable.MultiTenancy;
using Contable.Web.Models.Ui;
using Contable.Web.Session;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Contable.Configuration;
using Contable.Application.Extensions;

namespace Contable.Web.Controllers
{
    public class UiController : ContableControllerBase
    {
        private readonly IPerRequestSessionCache _sessionCache;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly IAccountAppService _accountAppService;
        private readonly LogInManager _logInManager;
        private readonly SignInManager _signInManager;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly IConfigurationRoot _configurationRoot;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UiController(
            IPerRequestSessionCache sessionCache,
            IMultiTenancyConfig multiTenancyConfig,
            IAccountAppService accountAppService,
            LogInManager logInManager,
            SignInManager signInManager,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            IWebHostEnvironment webHostEnvironment)
        {
            _sessionCache = sessionCache;
            _multiTenancyConfig = multiTenancyConfig;
            _accountAppService = accountAppService;
            _logInManager = logInManager;
            _signInManager = signInManager;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _webHostEnvironment = webHostEnvironment;
            _configurationRoot = webHostEnvironment.GetAppConfiguration();
        }

        [DisableAuditing]
        public async Task<IActionResult> Index()
        {
            var model = new HomePageModel
            {
                LoginInformation = await _sessionCache.GetCurrentLoginInformationsAsync(),
                IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled
            };

            if (model.LoginInformation?.User == null)
            {
                return RedirectToAction("Login");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ReturnUrl = returnUrl;
            }

            ViewBag.Key = _configurationRoot["Authentication:AES:Key"];
            ViewBag.Token = _configurationRoot["Authentication:AES:IV"];

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = "")
        {
            var key = _configurationRoot["Authentication:AES:Key"];
            var iv = _configurationRoot["Authentication:AES:IV"];

            try
            {
                model.UserNameOrEmailAddress = HelperExtensions.DecryptStringAES(model.RequestUser, key, iv);
                model.Password = HelperExtensions.DecryptStringAES(model.RequestPassword, key, iv);
            }
            catch
            {
                throw new UserFriendlyException(L("InvalidUserNameOrPassword", model.TenancyName));
            }

            var loginResult = await GetLoginResultAsync(model.UserNameOrEmailAddress, model.Password, model.TenancyName);

            if (loginResult.User.ShouldChangePasswordOnNextLogin)
            {
                throw new UserFriendlyException(L("RequiresPasswordChange"));
            }

            var signInResult = await _signInManager.SignInOrTwoFactorAsync(loginResult, model.RememberMe);

            if (signInResult.RequiresTwoFactor)
            {
                throw new UserFriendlyException(L("RequiresTwoFactorAuth"));
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index");
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
    }
}