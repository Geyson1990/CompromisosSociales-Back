using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilitySocialConflictLocationDataDto : EntityDto
    {
        public UtilityTerritorialUnitDto TerritorialUnit { get; set; }
        public UtilityDepartmentDto Department { get; set; }
        public UtilityProvinceDto Province { get; set; }
        public UtilityDistrictDto District { get; set; }
    }
}
