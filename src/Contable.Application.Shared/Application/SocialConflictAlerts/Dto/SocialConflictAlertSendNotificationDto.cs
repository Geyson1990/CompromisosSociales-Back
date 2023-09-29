using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictAlerts.Dto
{
    public class SocialConflictAlertSendNotificationDto : EntityDto
    {
        public string Subject { get; set; }
        public string Template { get; set; }
        public List<string> To { get; set; }
        public List<string> Copy { get; set; }
    }
}
