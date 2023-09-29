using Contable.Application;
using Contable.Application.Utilities.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Net.Emailing
{
    public interface IAppEmailSender
    {
        Task SendEmail(string[] to, string[] cc, string subject, string body, EmailAttachment[] attachments);
        string CreateTaskTemplate(string template, TaskManagement task, List<UtilityPersonListDto> coordinators, List<UtilityPersonListDto> managers);
        string CreateSocialConflictTaskTemplate(string template, SocialConflictTaskManagement task, List<UtilityPersonListDto> coordinators, List<UtilityPersonListDto> managers);
    }
}
