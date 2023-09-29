using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.InterventionPlans.Dto
{
    public class InterventionPlanScheduleRelationDto : EntityDto
    {
        public string Schedule { get; set; }
        public DateTime ScheduleTime { get; set; }
        public InterventionPlanActivityRelationDto InterventionPlanActivity { get; set; }
        public string Activity { get; set; }
        public InterventionPlanEntityRelationDto InterventionPlanEntity { get; set; }
        public InterventionPlanAlertResponsibleRelationDto AlertResponsible { get; set; }
        public InterventionPlanDirectoryGovernmentRelationDto DirectoryGovernment { get; set; }
        public InterventionPlanMethodologyRelationDto InterventionPlanMethodology { get; set; }
        public string Entity { get; set; }
        public string Product { get; set; }
        public bool Remove { get; set; }
    }
}
