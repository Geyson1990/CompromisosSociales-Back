using Abp.Configuration;
using Abp.Dependency;
using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Abp.Runtime.Security;
using Abp.Net.Mail;
using Contable.Application;
using Contable.Application.Utilities.Dto;

namespace Contable.Net.Emailing
{
    public class AppEmailSender : ContableServiceBase, IAppEmailSender, ITransientDependency
    {
        private readonly ISettingManager _settingManager;
        private readonly IEmailSender _emailSender;
        public AppEmailSender(ISettingManager settingManager, IEmailSender emailSender)
        {
            _settingManager = settingManager;
            _emailSender = emailSender;
        }

        public async Task SendEmail(string[] to, string[] cc, string subject, string body, EmailAttachment[] attachments)
        {
           
            var client = new SmtpClient(Host, Port)
            {
                Credentials = new NetworkCredential(UserName, Password),
                EnableSsl = EnableSsl,
                Timeout = 60000
                
            };

            var message = new MailMessage()
            {
                From = new MailAddress(UserName),
                Subject = subject,
                Body =  body,
                IsBodyHtml = true,
                SubjectEncoding = Encoding.UTF8,
                BodyEncoding = Encoding.UTF8
             
            };

            foreach (var emailAddress in to)
                message.To.Add(new MailAddress(emailAddress));
            foreach (var emailAddress in cc)
                message.CC.Add(new MailAddress(emailAddress));

            if(attachments != null)
                foreach(var attachment in attachments)
                    message.Attachments.Add(new Attachment(new MemoryStream(attachment.Content), attachment.Name));
           
            
            await _emailSender.SendAsync(message);

            //await client.SendMailAsync(message);
        }

        private string Password => SimpleStringCipher.Instance.Decrypt(_settingManager.GetSettingValueForTenant(EmailSettingNames.Smtp.Password, ContableConsts.DefaultTenantId));
        private string Host => _settingManager.GetSettingValueForTenant(EmailSettingNames.Smtp.Host, ContableConsts.DefaultTenantId);
        private int Port => _settingManager.GetSettingValueForTenant<int>(EmailSettingNames.Smtp.Port, ContableConsts.DefaultTenantId);
        private string UserName => _settingManager.GetSettingValueForTenant(EmailSettingNames.Smtp.UserName, ContableConsts.DefaultTenantId);
        private bool EnableSsl => _settingManager.GetSettingValueForTenant<bool>(EmailSettingNames.Smtp.EnableSsl, ContableConsts.DefaultTenantId);

        #region Templates

        public string CreateTaskTemplate(string template, TaskManagement task, List<UtilityPersonListDto> coordinators, List<UtilityPersonListDto> managers)
        {
            template = template.Replace("TASK_NAME", task.Title);
            template = template.Replace("TASK_DESCRIPTION", task.Description);
            template = template.Replace("TASK_CREATION_TIME", task.CreationTime.ToString("dd/MM/yyyy"));
            template = template.Replace("TASK_START_TIME", task.StartTime.HasValue ? task.StartTime.Value.ToString("dd/MM/yyyy") : "");
            template = template.Replace("TASK_DEADLINE", task.Deadline.HasValue ? task.Deadline.Value.ToString("dd/MM/yyyy") : "");

            var coordinatorsText = "<span>";

            foreach (var coordinator in coordinators)
                coordinatorsText += "<br>" + coordinator.Name;

            coordinatorsText += "</span>";

            template = template.Replace("TASK_COORDINATORS", coordinators.Count == 0 ? "SIN COORDINADORES" : coordinatorsText);

            var managersText = "<span>";

            foreach (var manager in managers)
                managersText += "<br>" + manager.Name;

            managersText += "</span>";

            template = template.Replace("TASK_MANAGERS", managers.Count == 0 ? "SIN GESTORES" : managersText);

            return template;
        }

        public string CreateSocialConflictTaskTemplate(string template, SocialConflictTaskManagement task, List<UtilityPersonListDto> coordinators, List<UtilityPersonListDto> managers)
        {
            template = template.Replace("TASK_NAME", task.Title);
            template = template.Replace("TASK_DESCRIPTION", task.Description);
            template = template.Replace("TASK_CREATION_TIME", task.CreationTime.ToString("dd/MM/yyyy"));
            template = template.Replace("TASK_START_TIME", task.StartTime.HasValue ? task.StartTime.Value.ToString("dd/MM/yyyy") : "");
            template = template.Replace("TASK_DEADLINE", task.Deadline.HasValue ? task.Deadline.Value.ToString("dd/MM/yyyy") : "");

            var coordinatorsText = "<span>";

            foreach (var coordinator in coordinators)
                coordinatorsText += "<br>" + coordinator.Name;

            coordinatorsText += "</span>";

            template = template.Replace("TASK_COORDINATORS", coordinators.Count == 0 ? "SIN COORDINADORES" : coordinatorsText);

            var managersText = "<span>";

            foreach (var manager in managers)
                managersText += "<br>" + manager.Name;

            managersText += "</span>";

            template = template.Replace("TASK_MANAGERS", managers.Count == 0 ? "SIN GESTORES" : managersText);

            return template;
        }

        #endregion
    }
}
