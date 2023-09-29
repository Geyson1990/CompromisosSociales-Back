using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppProjectStageDetails")]
    public class ProjectStageDetail : FullAuditedEntity
    {
        [Column(TypeName = ProjectStageConsts.ProjectStageIdType)]
        public int ProjectStageId { get; set; }
        public ProjectStage ProjectStage { get; set; }

        [Column(TypeName = ProjectStageConsts.StaticVariableIdType)]
        public int StaticVariableId { get; set; }
        public StaticVariable StaticVariable { get; set; }
    }
}
