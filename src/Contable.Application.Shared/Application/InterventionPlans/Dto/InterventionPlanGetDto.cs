using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.InterventionPlans.Dto
{
    public class InterventionPlanGetDto : EntityDto
    {
        public InterventionPlanSocialConflictRelationDto SocialConflict { get; set; }
        public InterventionPlanSocialConflictSensibleRelationDto SocialConflictSensible { get; set; }
        public DateTime InterventionPlanTime { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int Year { get; set; }
        public int Count { get; set; }
        public string Code { get; set; }
        public string CaseName { get; set; }
        public InterventionPlanPersonRelationDto Person { get; set; }
        public List<InterventionPlanLocationRelationDto> Locations { get; set; }
        public List<InterventionPlanActorRelationDto> Actors { get; set; }
        public List<InterventionPlanStateRelationDto> States { get; set; }
        public List<InterventionPlanMethodologyRelationDto> Methodologies { get; set; }
        public List<InterventionPlanRiskRelationDto> Risks { get; set; }
        public List<InterventionPlanScheduleRelationDto> Schedules { get; set; }
        public List<InterventionPlanTeamRelationDto> Teams { get; set; }
        public List<InterventionPlanSolutionRelationDto> Solutions { get; set; }
        public InterventionPlanUserDto CreatorUser { get; set; }
        public InterventionPlanUserDto EditUser { get; set; }
    }
}
