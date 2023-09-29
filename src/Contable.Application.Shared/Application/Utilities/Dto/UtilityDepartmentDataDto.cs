using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilityDepartmentDataDto : EntityDto
    {
        public string Name { get; set; }
        public List<UtilityProvinceDataDto> Provinces { get; set; }
        public List<EntityDto> TerritorialUnitIds { get; set; }
    }
}
