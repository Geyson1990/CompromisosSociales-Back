using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppProjectRiskHistories")]
    public class ProjectRiskHistory : FullAuditedEntity
    {
        [Column(TypeName = ProjectRiskHistoryConsts.ProjectRiskIdType)]
        [ForeignKey("ProjectRisk")]
        public int ProjectRiskId { get; set; }
        public ProjectRisk ProjectRisk { get; set; }

        [Column(TypeName = ProjectRiskHistoryConsts.StageIdType)]
        [ForeignKey("Stage")]
        public int StageId { get; set; }
        public ProjectStage Stage { get; set; }

        [Column(TypeName = ProjectRiskHistoryConsts.EvaluatedTimeType)]
        public DateTime EvaluatedTime { get; set; }

        [Column(TypeName = ProjectRiskHistoryConsts.TotalType)]
        public decimal Total { get; set; }

        [Column(TypeName = ProjectRiskHistoryConsts.FixProbabilityRateType)]
        public decimal FixProbabilityRate { get; set; }

        [Column(TypeName = ProjectRiskHistoryConsts.ProbabilityType)]
        public decimal Probability { get; set; }

        [Column(TypeName = ProjectRiskHistoryConsts.ProbabilityWeight)]
        public decimal ProbabilityWeight { get; set; }

        [Column(TypeName = ProjectRiskHistoryConsts.ImpactType)]
        public decimal Impact { get; set; }

        [Column(TypeName = ProjectRiskHistoryConsts.FixImpactRateType)]
        public decimal FixImpactRate { get; set; }

        [Column(TypeName = ProjectRiskHistoryConsts.ImpactWeight)]
        public decimal ImpactWeight { get; set; }

        [Column(TypeName = ProjectRiskHistoryConsts.ValueType)]
        public decimal Value { get; set; }

        public List<ProjectRiskHistoryDetail> Details { get; set; }
    }
}
