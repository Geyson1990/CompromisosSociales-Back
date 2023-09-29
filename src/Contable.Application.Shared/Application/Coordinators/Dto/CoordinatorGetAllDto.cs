using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Coordinators.Dto
{
    public class CoordinatorGetAllDto : EntityDto
    {
        public string Document { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public bool Enabled { get; set; }
        public List<CoordinatorTerritorialUnitRelationDto> TerritorialUnits { get; set; }
    }
}
