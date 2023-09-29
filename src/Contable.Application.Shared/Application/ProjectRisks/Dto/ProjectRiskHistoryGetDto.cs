using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectRisks.Dto
{
    public class ProjectRiskHistoryGetDto : EntityDto
    {
        public ProjectRiskUserDto CreationUser { get; set; }
        public ProjectRiskHistoryStageGetDto Stage { get; set; }
        public DateTime EvaluatedTime { get; set; }
        public DateTime CreationTime { get; set; }
        public decimal Total { get; set; }
        public decimal FixProbabilityRate { get; set; }
        public decimal Probability { get; set; }
        public decimal ProbabilityWeight { get; set; }
        public decimal Impact { get; set; }
        public decimal FixImpactRate { get; set; }
        public decimal ImpactWeight { get; set; }
        public decimal Value { get; set; }
    }
}
