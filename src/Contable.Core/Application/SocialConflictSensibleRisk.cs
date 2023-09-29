using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictSensibleRisks")]
    public class SocialConflictSensibleRisk : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictSensibleRiskConsts.SocialConflictSensibleIdType)]
        [ForeignKey("SocialConflictSensible")]
        public int SocialConflictSensibleId { get; set; }
        public SocialConflictSensible SocialConflictSensible { get; set; }

        [Column(TypeName = SocialConflictSensibleRiskConsts.RiskIdType)]
        [ForeignKey("Risk")]
        public int RiskId { get; set; }
        public Risk Risk { get; set; }

        [Column(TypeName = SocialConflictSensibleRiskConsts.RiskTimeType)]
        public DateTime RiskTime { get; set; }

        [Column(TypeName = SocialConflictSensibleRiskConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = SocialConflictSensibleRiskConsts.VerificationType)]
        public bool Verification { get; set; }
    }
}
