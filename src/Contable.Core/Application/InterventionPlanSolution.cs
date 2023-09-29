using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppInterventionPlanSolutions")]
    public class InterventionPlanSolution : FullAuditedEntity
    {
        [Column(TypeName = InterventionPlanSolutionConsts.InterventionPlanIdType)]
        [ForeignKey("InterventionPlan")]
        public int InterventionPlanId { get; set; }
        public InterventionPlan InterventionPlan { get; set; }

        [Column(TypeName = InterventionPlanSolutionConsts.DescriptionType)]
        public string Description { get; set; }
    }
}
