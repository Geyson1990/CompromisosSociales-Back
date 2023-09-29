using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.InterventionPlans.Dto
{
    public class InterventionPlanDepartmentDto : EntityDto
    {
        public int[] TerritorialUnitIds { get; set; }
        public string Name { get; set; }
        public List<InterventionPlanProvinceDto> Provinces { get; set; }
    }
}
