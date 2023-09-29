using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppProjectRiskHistoryDetails")]
    public class ProjectRiskHistoryDetail : FullAuditedEntity
    {
        [Column(TypeName = ProjectRiskHistoryDetailConsts.ProjectRiskHistoryIdType)]
        [ForeignKey("ProjectRiskHistory")]
        public int ProjectRiskHistoryId { get; set; }
        public ProjectRiskHistory ProjectRiskHistory { get; set; }

        [Column(TypeName = ProjectRiskHistoryDetailConsts.ProjectStageDetailIdType)]
        [ForeignKey("ProjectStageDetail")]
        public int ProjectStageDetailId { get; set; }
        public ProjectStageDetail ProjectStageDetail { get; set; }

        [Column(TypeName = ProjectRiskHistoryDetailConsts.StaticVariableIdType)]
        [ForeignKey("StaticVariable")]
        public int StaticVariableId { get; set; }
        public StaticVariable StaticVariable { get; set; }

        [Column(TypeName = ProjectRiskHistoryDetailConsts.StaticVariableOptionIdType)]
        [ForeignKey("StaticVariableOption")]
        public int StaticVariableOptionId { get; set; }
        public StaticVariableOption StaticVariableOption { get; set; }

        [Column(TypeName = ProjectRiskHistoryDetailConsts.WeightType)]
        public decimal Weight { get; set; }

        [Column(TypeName = ProjectRiskHistoryDetailConsts.ValueType)]
        public decimal Value { get; set; }
    }
}
