using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppProjectRisks")]
    public class ProjectRisk : FullAuditedEntity
    {
        [Column(TypeName = ProjectRiskConsts.ProvinceIdType)]
        [ForeignKey("Province")]
        public int ProvinceId { get; set; }
        public Province Province { get; set; }

        [Column(TypeName = ProjectRiskConsts.StageIdType)]
        [ForeignKey("Stage")]
        public int? StageId { get; set; }
        public ProjectStage Stage { get; set; }

        [Column(TypeName = ProjectRiskConsts.CodeType)]
        public string Code { get; set; }

        [Column(TypeName = ProjectRiskConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = ProjectRiskConsts.EvaluatedTimeType)]
        public DateTime EvaluatedTime { get; set; }

        [Column(TypeName = ProjectRiskConsts.TotalType)]
        public decimal Total { get; set; }

        [Column(TypeName = ProjectRiskConsts.FixProbabilityRateType)]
        public decimal FixProbabilityRate { get; set; }

        [Column(TypeName = ProjectRiskConsts.ProbabilityType)]
        public decimal Probability { get; set; }

        [Column(TypeName = ProjectRiskConsts.ProbabilityWeight)]
        public decimal ProbabilityWeight { get; set; }

        [Column(TypeName = ProjectRiskConsts.ImpactType)]
        public decimal Impact { get; set; }

        [Column(TypeName = ProjectRiskConsts.FixImpactRateType)]
        public decimal FixImpactRate { get; set; }

        [Column(TypeName = ProjectRiskConsts.ImpactWeight)]
        public decimal ImpactWeight { get; set; }

        [Column(TypeName = ProjectRiskConsts.ValueType)]
        public decimal Value { get; set; }

        public List<ProjectRiskDetail> Details { get;}

        public List<ProjectRiskHistory> Histories { get;}
    }
}
