using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppInterventionPlanSchedules")]
    public class InterventionPlanSchedule : FullAuditedEntity
    {
        [Column(TypeName = InterventionPlanScheduleConsts.InterventionPlanIdType)]
        [ForeignKey("InterventionPlan")]
        public int InterventionPlanId { get; set; }
        public InterventionPlan InterventionPlan { get; set; }

        [Column(TypeName = InterventionPlanScheduleConsts.ScheduleType)]
        public string Schedule { get; set; }

        [Column(TypeName = InterventionPlanScheduleConsts.ScheduleTimeType)]
        public DateTime ScheduleTime { get; set; }

        [Column(TypeName = InterventionPlanScheduleConsts.InterventionPlanActivityIdType)]
        [ForeignKey("InterventionPlanActivity")]
        public int InterventionPlanActivityId { get; set; }
        public InterventionPlanActivity InterventionPlanActivity { get; set; }

        [Column(TypeName = InterventionPlanScheduleConsts.ActivityType)]
        public string Activity { get; set; }

        [Column(TypeName = InterventionPlanScheduleConsts.InterventionPlanEntityIdType)]
        [ForeignKey("InterventionPlanEntity")]
        public int InterventionPlanEntityId { get; set; }
        public InterventionPlanEntity InterventionPlanEntity { get; set; }

        [Column(TypeName = InterventionPlanScheduleConsts.AlertResponsibleIdType)]
        [ForeignKey("AlertResponsible")]
        public int? AlertResponsibleId { get; set; }
        public AlertResponsible AlertResponsible { get; set; }

        [Column(TypeName = InterventionPlanScheduleConsts.DirectoryGovernmentIdType)]
        [ForeignKey("DirectoryGovernment")]
        public int? DirectoryGovernmentId { get; set; }
        public DirectoryGovernment DirectoryGovernment { get; set; }

        [Column(TypeName = InterventionPlanScheduleConsts.InterventionPlanMethodologyIdType)]
        [ForeignKey("InterventionPlanMethodology")]
        public int? InterventionPlanMethodologyId { get; set; }
        public InterventionPlanMethodology InterventionPlanMethodology { get; set; }

        [Column(TypeName = InterventionPlanScheduleConsts.EntityType)]
        public string Entity { get; set; }

        [Column(TypeName = InterventionPlanScheduleConsts.ProductType)]
        public string Product { get; set; }
    }
}
