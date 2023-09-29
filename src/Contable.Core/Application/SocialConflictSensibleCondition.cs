using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictSensibleConditions")]
    public class SocialConflictSensibleCondition : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictSensibleConditionConsts.SocialConflictSensibleIdType)]
        [ForeignKey("SocialConflictSensible")]
        public int SocialConflictSensibleId { get; set; }
        public SocialConflictSensible SocialConflictSensible { get; set; }

        [Column(TypeName = SocialConflictSensibleConditionConsts.Type)]
        public ConditionType Type { get; set; }

        [Column(TypeName = SocialConflictSensibleConditionConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = SocialConflictSensibleConditionConsts.ConditionTimeType)]
        public DateTime ConditionTime { get; set; }

        [Column(TypeName = SocialConflictSensibleConditionConsts.VerificationType)]
        public bool Verification { get; set; }
    }
}
