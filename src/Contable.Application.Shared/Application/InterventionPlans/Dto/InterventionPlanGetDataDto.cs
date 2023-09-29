using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.InterventionPlans.Dto
{
    public class InterventionPlanGetDataDto
    {
        public InterventionPlanGetDto InterventionPlan { get; set; }
        public List<InterventionPlanActorMovementRelationDto> ActorMovements { get; set; }
        public List<InterventionPlanActorTypeRelationDto> ActorTypes { get; set; }
        public List<InterventionPlanOptionRelationDto> Options { get; set; }
        public List<InterventionPlanDepartmentDto> Departments { get; set; }
        public List<InterventionPlanPersonRelationDto> Persons { get; set; }
        public List<InterventionPlanTerritorialUnitRelationDto> TerritorialUnits { get; set; }
        public List<InterventionPlanRiskLevelRelationDto> Risks { get; set; }
        public List<InterventionPlanActivityRelationDto> Activities { get; set; }
        public List<InterventionPlanEntityRelationDto> Entities { get; set; }
        public List<InterventionPlanAlertResponsibleRelationDto> AlertResponsibles { get; set; }
        public List<InterventionPlanRoleRelationDto> Roles { get; set; }
    }
}