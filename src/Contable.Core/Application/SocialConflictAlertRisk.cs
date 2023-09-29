using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictAlertRisks")]
    public class SocialConflictAlertRisk : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictAlertRiskConsts.SocialConflictAlertIdType)]
        [ForeignKey("SocialConflictAlert")]
        public int SocialConflictAlertId { get; set; }
        public SocialConflictAlert SocialConflictAlert { get; set; }

        [Column(TypeName = SocialConflictAlertRiskConsts.AlertRiskIdType)]
        [ForeignKey("AlertRisk")]
        public int AlertRiskId { get; set; }
        public AlertRisk AlertRisk { get; set; }

        [Column(TypeName = SocialConflictAlertRiskConsts.RiskTimeType)]
        public DateTime RiskTime { get; set; }

        [Column(TypeName = SocialConflictAlertRiskConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = SocialConflictAlertRiskConsts.ObservationType)]
        public string Observation { get; set; }
    }
}
