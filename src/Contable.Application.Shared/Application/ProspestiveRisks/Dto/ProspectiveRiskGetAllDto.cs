using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProspestiveRisks.Dto
{
    public class ProspectiveRiskGetAllDto : NullableIdDto
    {
        public List<ProspectiveRiskTerritorialUnitDto> TerritorialUnits { get; set; }
        public ProspectiveRiskDepartmentDto Department { get; set; }
        public ProspectiveRiskProvinceDto Province { get; set; }
        public DateTime? EvaluatedTime { get; set; }
        public DateTime? CreationTime { get; set; }
        public decimal Value { get; set; }
    }
}
