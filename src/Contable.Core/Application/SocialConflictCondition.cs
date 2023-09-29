using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictConditions")]
    public class SocialConflictCondition : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictConditionConsts.SocialConflictIdType)]
        [ForeignKey("SocialConflict")]
        public int SocialConflictId { get; set; }
        public SocialConflict SocialConflict { get; set; }

        [Column(TypeName = SocialConflictConditionConsts.Type)]
        public ConditionType Type { get; set; }

        [Column(TypeName = SocialConflictConditionConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = SocialConflictConditionConsts.ConditionTimeType)]
        public DateTime ConditionTime { get; set; }

        [Column(TypeName = SocialConflictConditionConsts.VerificationType)]
        public bool Verification { get; set; }
    }
}
