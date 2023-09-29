using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppProjectRiskDetails")]
    public class ProjectRiskDetail : FullAuditedEntity
    {
        [Column(TypeName = ProjectRiskDetailConsts.ProjectRiskIdType)]
        [ForeignKey("ProjectRisk")]
        public int ProjectRiskId { get; set; }
        public ProjectRisk ProjectRisk { get; set; }

        [Column(TypeName = ProjectRiskDetailConsts.ProjectStageDetailIdType)]
        [ForeignKey("ProjectStageDetail")]
        public int ProjectStageDetailId { get; set; }
        public ProjectStageDetail ProjectStageDetail { get; set; }

        [Column(TypeName = ProjectRiskDetailConsts.StaticVariableOptionIdType)]
        [ForeignKey("StaticVariableOption")]
        public int StaticVariableOptionId { get; set; }
        public StaticVariableOption StaticVariableOption { get; set; }

        [Column(TypeName = ProjectRiskDetailConsts.EnabledType)]
        public bool Enabled { get; set; }

        [Column(TypeName = ProjectRiskDetailConsts.ValueType)]
        public decimal Value { get; set; }
    }
}
