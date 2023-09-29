using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectRisks.Dto
{
    public class ProjectRiskGetDto : EntityDto
    {
        public DateTime CreationTime { get; set; }
        public DateTime LastModificationTime { get; set; }
        public ProjectRiskUserDto CreationUser { get; set; }
        public ProjectRiskUserDto EditionUser { get; set; }
        public int DepartmentId { get; set; }
        public int ProvinceId { get; set; }
        public int StageId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime EvaluatedTime { get; set; }
        public decimal FixProbabilityRate { get; set; }
        public decimal Probability { get; set; }
        public decimal Impact { get; set; }
        public decimal FixImpactRate { get; set; }
        public decimal Total { get; set; }
        public decimal Value { get; set; }
        public List<ProjectRiskDetailDto> Details { get; set; }
    }
}
