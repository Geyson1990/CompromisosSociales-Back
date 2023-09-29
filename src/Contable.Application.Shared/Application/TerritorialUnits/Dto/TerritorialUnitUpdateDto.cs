using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.TerritorialUnits.Dto
{
    public class TerritorialUnitUpdateDto : EntityDto
    {
        public string Name { get; set; }

        public List<TerritorialUnitCoordinatorRelationDto> Coordinators { get; set; }
    }
}
