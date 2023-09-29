using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.InterventionPlans.Dto
{
    public class InterventionPlanAlertResponsibleRelationDto : EntityDto
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
