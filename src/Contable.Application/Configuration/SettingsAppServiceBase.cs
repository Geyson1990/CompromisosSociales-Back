using Abp.Configuration;
using Abp.Net.Mail;
using Abp.Runtime.Security;
using Contable.Configuration.Dto;
using Contable.Configuration.Host.Dto;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Configuration
{
    public abstract class SettingsAppServiceBase : ContableAppServiceBase
    {
        private readonly IEmailSender _emailSender;
        private readonly IAppConfigurationAccessor _configurationAccessor;
        private readonly ISettingManager _settingManager;


        protected SettingsAppServiceBase(
            IEmailSender emailSender,
            IAppConfigurationAccessor configurationAccessor, ISettingManager settingManager)
        {
            _emailSender = emailSender;
            _configurationAccessor = configurationAccessor;
            _settingManager = settingManager;
        }

        #region Send Test Email

        public async Task SendTestEmail(SendTestEmailInput input)
        {
            await _emailSender.SendAsync(input.EmailAddress, "PCM", "Correo electrónico de prueba");
        }

        public ExternalLoginSettingsDto GetEnabledSocialLoginSettings()
        {
            var dto = new ExternalLoginSettingsDto();
            if (!bool.Parse(_configurationAccessor.Configuration["Authentication:AllowSocialLoginSettingsPerTenant"]))
            {
                return dto;
            }

            if (IsSocialLoginEnabled("Facebook"))
            {
                dto.EnabledSocialLoginSettings.Add("Facebook");
            }

            if (IsSocialLoginEnabled("Google"))
            {
                dto.EnabledSocialLoginSettings.Add("Google");
            }

            if (IsSocialLoginEnabled("Twitter"))
            {
                dto.EnabledSocialLoginSettings.Add("Twitter");
            }

            if (IsSocialLoginEnabled("Microsoft"))
            {
                dto.EnabledSocialLoginSettings.Add("Microsoft");
            }

            return dto;
        }

        private bool IsSocialLoginEnabled(string name)
        {
            return _configurationAccessor.Configuration.GetSection("Authentication:" + name).Exists() &&
                   bool.Parse(_configurationAccessor.Configuration["Authentication:" + name + ":IsEnabled"]);
        }

        public async Task SendEmail(SendTestEmailInput input, string subject, string body)
        {
            var credentials = new NetworkCredential(UserName, Password);
            credentials.Domain = Host;
            credentials.UserName = UserName;
            credentials.Password = Password;
            

            var client = new SmtpClient(Host, Port)
            {
                Credentials = credentials,
                EnableSsl = EnableSsl,
                Timeout = 600,

            };

            var message = new MailMessage()
            {
                From = new MailAddress(UserName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
                SubjectEncoding = Encoding.UTF8,
                BodyEncoding = Encoding.UTF8              
            };


            message.To.Add(new MailAddress(input.EmailAddress));


            await _emailSender.SendAsync(message);

            //await client.SendMailAsync(message);
        }


        private string Password => SimpleStringCipher.Instance.Decrypt(_settingManager.GetSettingValueForTenant(EmailSettingNames.Smtp.Password, ContableConsts.DefaultTenantId));
        private string Host => _settingManager.GetSettingValueForTenant(EmailSettingNames.Smtp.Host, ContableConsts.DefaultTenantId);
        private int Port => _settingManager.GetSettingValueForTenant<int>(EmailSettingNames.Smtp.Port, ContableConsts.DefaultTenantId);
        private string UserName => _settingManager.GetSettingValueForTenant(EmailSettingNames.Smtp.UserName, ContableConsts.DefaultTenantId);
        private bool EnableSsl => _settingManager.GetSettingValueForTenant<bool>(EmailSettingNames.Smtp.EnableSsl, ContableConsts.DefaultTenantId);
        #endregion
    }
}
