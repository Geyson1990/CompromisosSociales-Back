using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppInterventionPlans")]
    public class InterventionPlan : FullAuditedEntity
    {
        [Column(TypeName = InterventionPlanConsts.SocialConflictIdType)]
        [ForeignKey("SocialConflict")]
        public int? SocialConflictId { get; set; }
        public SocialConflict SocialConflict { get; set; }

        [Column(TypeName = InterventionPlanConsts.SocialConflictSensibleIdType)]
        [ForeignKey("SocialConflictSensible")]
        public int? SocialConflictSensibleId { get; set; }  
        public SocialConflictSensible SocialConflictSensible { get; set; }

        [Column(TypeName = InterventionPlanConsts.CodeType)]
        public string Code { get; set; }

        [Column(TypeName = InterventionPlanConsts.YearType)]
        public int Year { get; set; }

        [Column(TypeName = InterventionPlanConsts.CountType)]
        public int Count { get; set; }

        [Column(TypeName = InterventionPlanConsts.GenerationType)]
        public bool Generation { get; set; }

        [Column(TypeName = InterventionPlanConsts.InterventionPlanTimeType)]
        public DateTime InterventionPlanTime { get; set; }

        [Column(TypeName = InterventionPlanConsts.CaseNameType)]
        public string CaseName { get; set; }

        [Column(TypeName = InterventionPlanConsts.SiteType)]
        public ConflictSite Site { get; set; }

        [Column(TypeName = InterventionPlanConsts.PersonIdType)]
        [ForeignKey("Person")]
        public int? PersonId { get; set; }
        public Person Person { get; set; }

        [Column(TypeName = InterventionPlanConsts.LastInterventionPlanRiskIdType)]
        public int? LastInterventionPlanRiskId { get; set; }

        public List<InterventionPlanLocation> Locations { get; set; }
        public List<InterventionPlanActor> Actors { get; set; }
        public List<InterventionPlanState> States { get; set; }
        public List<InterventionPlanRisk> Risks { get; set; }
        public List<InterventionPlanMethodology> Methodologies { get; set; }
        public List<InterventionPlanSolution> Solutions { get; set; }
        public List<InterventionPlanSchedule> Schedules { get; set; }
        public List<InterventionPlanTeam> Teams { get; set; }
    }
}
