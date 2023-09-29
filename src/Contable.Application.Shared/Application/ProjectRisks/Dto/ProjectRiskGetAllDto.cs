using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectRisks.Dto
{
    public class ProjectRiskGetAllDto : EntityDto
    {
        public ProjectRiskProvinceGetAllDto Province { get; set; }
        public ProjectRiskStageGetAllDto Stage { get; set; }
        public DateTime CreationTime { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime EvaluatedTime { get; set; }
        public decimal Total { get; set; }
        public decimal Probability { get; set; }
        public decimal Impact { get; set; }
        public decimal Value { get; set; }

    }
}
