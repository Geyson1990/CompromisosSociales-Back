using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictAlertHistories")]
    public class SocialConflictAlertHistory : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictAlertHistoryConsts.SocialConflictAlertIdType)]
        [ForeignKey("SocialConflictAlert")]
        public int SocialConflictAlertId { get; set; }
        public SocialConflictAlert SocialConflictAlert { get; set; }

        [Column(TypeName = SocialConflictAlertHistoryConsts.CodeType)]
        public string Code { get; set; }

        [Column(TypeName = SocialConflictAlertHistoryConsts.SubjectType)]
        public string Subject { get; set; }

        [Column(TypeName = SocialConflictAlertHistoryConsts.TemplateType)]
        public string Template { get; set; }

        [Column(TypeName = SocialConflictAlertHistoryConsts.ToType)]
        public string To { get; set; }

        [Column(TypeName = SocialConflictAlertHistoryConsts.CopyType)]
        public string Copy { get; set; }

        [Column(TypeName = SocialConflictAlertHistoryConsts.FileType)]
        public string Files { get; set; }
    }
}
