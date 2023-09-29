using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.TerritorialUnits.Dto
{
    public class TerritorialUnitDepartmentRelationDto : EntityDto
    {
        public TerritorialUnitDepartmentDto Department { get; set; }
    }
}
