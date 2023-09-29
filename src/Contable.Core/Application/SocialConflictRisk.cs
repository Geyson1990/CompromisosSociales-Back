using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictRisks")]
    public class SocialConflictRisk : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictRiskConsts.SocialConflictIdType)]
        [ForeignKey("SocialConflict")]
        public int SocialConflictId { get; set; }
        public SocialConflict SocialConflict { get; set; }

        [Column(TypeName = SocialConflictRiskConsts.RiskIdType)]
        [ForeignKey("Risk")]
        public int RiskId { get; set; }
        public Risk Risk { get; set; }

        [Column(TypeName = SocialConflictRiskConsts.RiskTimeType)]
        public DateTime RiskTime { get; set; }

        [Column(TypeName = SocialConflictRiskConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = SocialConflictRiskConsts.VerificationType)]
        public bool Verification { get; set; }
    }
}
