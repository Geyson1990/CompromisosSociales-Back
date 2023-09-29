using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.InterventionPlans.Dto
{
    public class InterventionPlanLocationReferenceDto
    {
        public InterventionPlanDepartmentDto Department { get; set; }
        public InterventionPlanProvinceDto Province { get; set; }
        public InterventionPlanDistrictDto District { get; set; }
        public InterventionPlanTerritorialUnitRelationDto TerritorialUnit { get; set; }
        public InterventionPlanRegionRelationDto Region { get; set; }
        public string Ubication { get; set; }
    }
}
