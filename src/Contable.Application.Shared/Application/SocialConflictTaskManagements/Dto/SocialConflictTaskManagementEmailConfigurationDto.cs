using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictTaskManagements.Dto
{
    public class SocialConflictTaskManagementEmailConfigurationDto : EntityDto
    {
        public string Subject { get; set; }
        public string Template { get; set; }
    }
}
