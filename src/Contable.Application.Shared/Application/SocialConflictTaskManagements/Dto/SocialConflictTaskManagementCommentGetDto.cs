using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictTaskManagements.Dto
{
    public class SocialConflictTaskManagementCommentGetDto : EntityDto
    {
        public DateTime CreationTime { get; set; }
        public string Description { get; set; }
        public SocialConflictTaskManagementUserDto User { get; set; }
        public CommentType Type { get; set; }
    }
}
