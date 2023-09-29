using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Extensions;
using Abp.Runtime.Security;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Zero.Configuration;
using Microsoft.AspNetCore.Identity;
using Contable.Authorization.Accounts.Dto;
using Contable.Authorization.Impersonation;
using Contable.Authorization.Users;
using Contable.Configuration;
using Contable.Debugging;
using Contable.MultiTenancy;
using Contable.Security.Recaptcha;
using Contable.Url;
using Contable.Authorization.Delegation;
using Abp.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Contable.Application.Extensions;

namespace Contable.Authorization.Accounts
{
    public class AccountAppService : ContableAppServiceBase, IAccountAppService
    {
        private readonly IUserEmailer _userEmailer;
        private readonly IConfigurationRoot _configurationRoot;

        public AccountAppService(IUserEmailer userEmailer, IWebHostEnvironment webHostEnvironment)
        {
            _userEmailer = userEmailer;
            _configurationRoot = webHostEnvironment.GetAppConfiguration();
        }

        public async Task SendPasswordResetCode(SendPasswordResetCodeInput input)
        {
            if (string.IsNullOrWhiteSpace(input.EmailAddress))
                return;

            var key = _configurationRoot["Authentication:AES:Key"];
            var iv = _configurationRoot["Authentication:AES:IV"];

            try
            {
                input.EmailAddress = HelperExtensions.DecryptStringAES(cipherText: input.EmailAddress, keyText: key, ivText: iv);
            }
            catch
            {
                throw new UserFriendlyException("Aviso", "El correo electrónico ingresado es inválido");
            }

            var user = await UserManager.FindByEmailAsync(input.EmailAddress);

            if (user == null)
                return;

            var minutesForNewNotification = Application.UserConsts.NewNotificationMinutes - Application.UserConsts.NotificationValidationMinutes;

            if (user.PasswordResetTime.HasValue && user.PasswordResetTime.Value.AddMinutes(minutesForNewNotification) > DateTime.Now)
                return;

            await _userEmailer.SendPasswordResetAsync(user);
        }

        public async Task<ResetPasswordOutput> ResetPassword(ResetPasswordInput input)
        {
            if (string.IsNullOrWhiteSpace(input.ResetCode))
                throw new UserFriendlyException("Aviso", "El código de verificación es inválido");
            if (string.IsNullOrWhiteSpace(input.EmailAddress))
                throw new UserFriendlyException("Aviso", "El correo electrónico es inválido");
            if (string.IsNullOrWhiteSpace(input.Password))
                throw new UserFriendlyException("Aviso", "La contraseña es inválida");

            var key = _configurationRoot["Authentication:AES:Key"];
            var iv = _configurationRoot["Authentication:AES:IV"];

            try
            {
                input.ResetCode = HelperExtensions.DecryptStringAES(cipherText: input.ResetCode, keyText: key, ivText: iv);
                input.Password = HelperExtensions.DecryptStringAES(cipherText: input.Password, keyText: key, ivText: iv);
                input.EmailAddress = HelperExtensions.DecryptStringAES(cipherText: input.EmailAddress, keyText: key, ivText: iv);
            }
            catch
            {
                throw new UserFriendlyException("Aviso", "Los datos ingresados son inválidos");
            }

            var user = await UserManager.FindByEmailAsync(input.EmailAddress);

            if (user == null || user.PasswordResetCode.IsNullOrEmpty() || user.PasswordResetCode != input.ResetCode)
                throw new UserFriendlyException("Aviso", "El código para restablecer la contraseña es inválido");
            if (user.PasswordResetTime.HasValue == false || user.PasswordResetTime.Value < DateTime.Now)
                throw new UserFriendlyException("Aviso", "El código para restablecer la contraseña ha expirado");

            await UserManager.InitializeOptionsAsync(AbpSession.TenantId);

            CheckErrors(await UserManager.ChangePasswordAsync(user, input.Password));

            user.PasswordResetCode = null;
            user.PasswordResetTime = null;
            user.IsEmailConfirmed = true;
            user.ShouldChangePasswordOnNextLogin = false;

            await UserManager.UpdateAsync(user);

            return new ResetPasswordOutput
            {
                CanLogin = user.IsActive
            };
        }
    }
}