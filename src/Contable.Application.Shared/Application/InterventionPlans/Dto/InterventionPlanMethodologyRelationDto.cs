using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.InterventionPlans.Dto
{
    public class InterventionPlanMethodologyRelationDto : EntityDto
    {
        public string Description { get; set; }
        public string Methodology { get; set; }
        public InterventionPlanOptionRelationDto InterventionPlanOption { get; set; }
        public bool Remove { get; set; }
    }
}
