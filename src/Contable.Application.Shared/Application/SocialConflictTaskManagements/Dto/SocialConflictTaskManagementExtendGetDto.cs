using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictTaskManagements.Dto
{
    public class SocialConflictTaskManagementExtendGetDto : EntityDto
    {
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
    }
}
