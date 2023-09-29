using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppInteventionPlanMethodologies")]
    public class InterventionPlanMethodology : FullAuditedEntity
    {
        [Column(TypeName = InterventionPlanMethodologyConsts.InterventionPlanIdType)]
        [ForeignKey("InterventionPlan")]
        public int InterventionPlanId { get; set; }
        public InterventionPlan InterventionPlan { get; set; }

        [Column(TypeName = InterventionPlanMethodologyConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = InterventionPlanMethodologyConsts.MethodologyType)]
        public string Methodology { get; set; }

        [Column(TypeName = InterventionPlanMethodologyConsts.InterventionPlanOptionIdType)]
        [ForeignKey("InterventionPlanOption")]
        public int InterventionPlanOptionId { get; set; }
        public InterventionPlanOption InterventionPlanOption { get; set; }
    }
}
