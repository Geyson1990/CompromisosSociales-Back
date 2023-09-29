using Abp.Domain.Entities.Auditing;
using Contable.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictTaskManagementComments")]
    public class SocialConflictTaskManagementComment : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictTaskManagementCommentConsts.SocialConflictTaskManagementIdType)]
        [ForeignKey("SocialConflictTaskManagement")]
        public int SocialConflictTaskManagementId { get; set; }
        public SocialConflictTaskManagement SocialConflictTaskManagement { get; set; }

        [Column(TypeName = SocialConflictTaskManagementCommentConsts.UserIdType)]
        [ForeignKey("User")]
        public long UserId { get; set; }
        public User User { get; set; }

        [Column(TypeName = SocialConflictTaskManagementCommentConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = SocialConflictTaskManagementCommentConsts.Type)]
        public CommentType Type { get; set; }
    }
}
