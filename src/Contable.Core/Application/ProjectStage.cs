using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppProjectStages")]
    public class ProjectStage : FullAuditedEntity
    {
        [Column(TypeName = ProjectStageConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = ProjectStageConsts.IndexType)]
        public int Index { get; set; }

        [Column(TypeName = ProjectStageConsts.EnabledType)]
        public bool Enabled { get; set; }

        public List<ProjectStageDetail> Details { get; set; }
    }
}
