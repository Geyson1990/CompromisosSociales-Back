using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Coordinators.Dto
{
    public class CoordinatorTerritorialUnitRelationDto : EntityDto
    {
        public CoordinatorTerritorialUnitDto TerritorialUnit { get; set; }
        public bool Remove { get; set; }
    }
}
