using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictTaskManagements.Dto
{
    public class SocialConflictTaskManagementCommentUpdateDto : EntityDto
    {
        public string Description { get; set; } 
        public EntityDto SocialConflictTaskManagement { get; set; }
    }
}
