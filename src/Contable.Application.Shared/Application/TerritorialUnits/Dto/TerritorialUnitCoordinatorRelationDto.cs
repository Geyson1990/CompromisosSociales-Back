using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.TerritorialUnits.Dto
{
    public class TerritorialUnitCoordinatorRelationDto : EntityDto
    {
        public TerritorialUnitCoordinatorDto Person { get; set; }
        public bool Remove { get; set; }
    }
}
