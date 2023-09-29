using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictTaskManagements.Dto
{
    public class SocialConflictTaskManagementCommentCreateDto
    {
        public string Description { get; set; }
        public SocialConflictTaskManagementRelationDto SocialConflictTaskManagement { get; set; }
    }
}
