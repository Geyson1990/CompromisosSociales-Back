using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictTaskManagementHistories")]
    public class SocialConflictTaskManagementHistory : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictTaskManagementHistoryConsts.SocialConflictTaskManagementIdType)]
        [ForeignKey("SocialConflictTaskManagement")]
        public int SocialConflictTaskManagementId { get; set; }
        public SocialConflictTaskManagement SocialConflictTaskManagement { get; set; }

        [Column(TypeName = SocialConflictTaskManagementHistoryConsts.SubjectType)]
        public string Subject { get; set; }

        [Column(TypeName = SocialConflictTaskManagementHistoryConsts.TemplateType)]
        public string Template { get; set; }

        [Column(TypeName = SocialConflictTaskManagementHistoryConsts.ToType)]
        public string To { get; set; }

        [Column(TypeName = SocialConflictTaskManagementHistoryConsts.CopyType)]
        public string Copy { get; set; }
    }
}
