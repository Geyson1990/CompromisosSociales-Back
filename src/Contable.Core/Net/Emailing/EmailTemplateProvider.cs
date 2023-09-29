using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using Abp.Dependency;
using Abp.Extensions;
using Abp.IO.Extensions;
using Abp.Reflection.Extensions;
using ApplicationBase.Net.Emailing;
using Contable.Configuration;
using Contable.Url;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Contable.Net.Emailing
{
    public class EmailTemplateProvider : IEmailTemplateProvider, ISingletonDependency
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmailTemplateProvider(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string GetEmailActivationTemplate(string name, string surname, string emailAddress, string password)
        {
            var separator = Path.DirectorySeparatorChar;
            var directory = $@"{_webHostEnvironment.ContentRootPath}{separator}Templates{separator}Registration.html";
            var template = File.ReadAllText(directory);

            template = template.Replace("{{TEMPLATE_TITLE}}", "Credenciales de acceso");
            template = template.Replace("{{USER_NAME}}", name);
            template = template.Replace("{{USER_SURNAME}}", surname);
            template = template.Replace("{{USER_EMAIL_ADDRESS}}", emailAddress);
            template = template.Replace("{{MAIL_REGISTRATION_CODE}}", password);

            return template;
        }

        public string GetEmailPasswordResetTemplate(string name, string surname, string emailAddress, string passwordResetCode)
        {
            var separator = Path.DirectorySeparatorChar;
            var directory = $@"{_webHostEnvironment.ContentRootPath}{separator}Templates{separator}PassworReset.html";
            var template = File.ReadAllText(directory);

            template = template.Replace("{{TEMPLATE_TITLE}}", "Restablecer credenciales");
            template = template.Replace("{{USER_NAME}}", name);
            template = template.Replace("{{USER_SURNAME}}", surname);
            template = template.Replace("{{USER_EMAIL_ADDRESS}}", emailAddress);
            template = template.Replace("{{MAIL_RESET_CODE}}", passwordResetCode);

            return template;
        }
    }
}