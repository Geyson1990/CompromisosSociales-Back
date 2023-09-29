using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.InterventionPlans.Dto
{
    public class InterventionPlanTeamRelationDto : EntityDto
    {
        public string Document { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondSurname { get; set; }
        public InterventionPlanEntityRelationDto InterventionPlanEntity { get; set; }
        public InterventionPlanAlertResponsibleRelationDto AlertResponsible { get; set; }
        public InterventionPlanDirectoryGovernmentRelationDto DirectoryGovernment { get; set; }
        public string Entity { get; set; }
        public string Job { get; set; }
        public InterventionPlanRoleRelationDto InterventionPlanRole { get; set; }
        public string Role { get; set; }
        public bool Remove { get; set; }
    }
}
