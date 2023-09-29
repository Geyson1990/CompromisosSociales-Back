using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProspestiveRisks.Dto
{
    public class ProspectiveRiskGetDto : NullableIdDto
    {
        public ProspectiveRiskDepartmentDto Department { get; set; }
        public ProspectiveRiskProvinceDto Province { get; set; }
        public ProspectiveRiskUserDto CreationUser { get; set; }
        public ProspectiveRiskUserDto EditionUser { get; set; }
        public DateTime? CreationTime { get; set; }
        public DateTime? EvaluatedTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public decimal FixRate { get; set; }
        public decimal Value { get; set; }
        public List<ProspectiveRiskTerritorialUnitDto> TerritorialUnits { get; set; }
        public List<ProspectiveRiskStaticVariableGetDto> Variables { get; set; }
        public List<ProspectiveRiskDetailGetDto> Details { get; set; }
    }
}
