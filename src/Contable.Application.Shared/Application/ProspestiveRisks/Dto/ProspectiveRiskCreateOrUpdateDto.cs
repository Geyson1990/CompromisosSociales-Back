using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProspestiveRisks.Dto
{
    public class ProspectiveRiskCreateOrUpdateDto : NullableIdDto
    {
        public ProspectiveRiskProvinceDto Province { get; set; }
        public DateTime? EvaluatedTime { get; set; }
        public decimal FixRate { get; set; }
        public List<ProspectiveRiskDetailCreateOrUpdateDto> Details { get; set; }
    }
}
