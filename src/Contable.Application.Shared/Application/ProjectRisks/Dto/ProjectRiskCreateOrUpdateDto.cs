using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectRisks.Dto
{
    public class ProjectRiskCreateOrUpdateDto : NullableIdDto
    {
        public int ProvinceId { get; set; }
        public int StageId { get; set; }
        public string Code { get; set; }    
        public string Name { get; set; }
        public DateTime? EvaluatedTime { get; set; }
        public decimal FixProbabilityRate { get; set; }
        public decimal FixImpactRate { get; set; }
        public List<ProjectRiskDetailCreateOrUpdateDto> Details { get; set; }
    }
}
