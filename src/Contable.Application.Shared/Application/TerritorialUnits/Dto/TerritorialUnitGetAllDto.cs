using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.TerritorialUnits.Dto
{
    public class TerritorialUnitGetAllDto : EntityDto
    {
        public string Name { get; set; }
        public List<TerritorialUnitDepartmentRelationDto> TerritorialUnitDepartments { get; set; }
    }
}
