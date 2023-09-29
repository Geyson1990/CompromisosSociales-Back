using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppInterventionPlanRisks")]
    public class InterventionPlanRisk : FullAuditedEntity
    {
        [Column(TypeName = InterventionPlanRiskConsts.InterventionPlanIdType)]
        [ForeignKey("InterventionPlan")]
        public int InterventionPlanId { get; set; }
        public InterventionPlan InterventionPlan { get; set; }

        [Column(TypeName = InterventionPlanRiskConsts.RiskIdType)]
        [ForeignKey("Risk")]
        public int RiskId { get; set; }
        public Risk Risk { get; set; }
    }
}
