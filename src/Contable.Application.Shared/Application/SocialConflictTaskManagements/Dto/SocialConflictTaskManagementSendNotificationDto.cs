using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictTaskManagements.Dto
{
    public class SocialConflictTaskManagementSendNotificationDto : EntityDto
    {
        public string Subject { get; set; }
        public string Template { get; set; }
        public List<string> To { get; set; }
        public List<string> Copy { get; set; }
    }
}
