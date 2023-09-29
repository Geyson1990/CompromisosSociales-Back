using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Localization;
using Abp.Net.Mail;
using Contable.Chat;
using Contable.Editions;
using Contable.Localization;
using Contable.MultiTenancy;
using System.Net.Mail;
using System.Web;
using Abp.Runtime.Security;
using Contable.Net.Emailing;
using Abp.UI;
using ApplicationBase.Net.Emailing;

namespace Contable.Authorization.Users
{
    /// <summary>
    /// Used to send email to users.
    /// </summary>
    public class UserEmailer : ContableServiceBase, IUserEmailer, ITransientDependency
    {
        private readonly IEmailTemplateProvider _emailTemplateProvider;
        private readonly IEmailSender _emailSender;

        public UserEmailer(IEmailTemplateProvider emailTemplateProvider, IEmailSender emailSender)
        {
            _emailTemplateProvider = emailTemplateProvider;
            _emailSender = emailSender;
        }

        public virtual async Task SendEmailActivationAsync(User user, string password)
        {
            var template = _emailTemplateProvider.GetEmailActivationTemplate(
                name: user.Name,
                surname: user.Surname,
                emailAddress: user.EmailAddress,
                password: password);

            await ReplaceBodyAndSend(user.EmailAddress, "Credenciales de acceso", template);
        }

        public async Task SendPasswordResetAsync(User user)
        {
            user.SetNewPasswordResetCode();

            var template = _emailTemplateProvider.GetEmailPasswordResetTemplate(
                name: user.Name,
                surname: user.Surname,
                emailAddress: user.EmailAddress,
                passwordResetCode: user.PasswordResetCode);

            await ReplaceBodyAndSend(user.EmailAddress, "Restablecer credenciales", template);
        }

        private async Task ReplaceBodyAndSend(string emailAddress, string subject, string emailTemplate)
        {
            await _emailSender.SendAsync(new MailMessage
            {
                To = { emailAddress },
                Subject = subject,
                Body = emailTemplate,
                IsBodyHtml = true
            });
        }
    }
}
