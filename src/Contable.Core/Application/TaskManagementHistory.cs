using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppTaskManagementHistories")]
    public class TaskManagementHistory : FullAuditedEntity
    {
        [Column(TypeName = TaskManagementHistoryConsts.TaskManagementIdType)]
        [ForeignKey("SocialConflictTaskManagement")]
        public long TaskManagementId { get; set; }
        public TaskManagement TaskManagement { get; set; }

        [Column(TypeName = TaskManagementHistoryConsts.SubjectType)]
        public string Subject { get; set; }

        [Column(TypeName = TaskManagementHistoryConsts.TemplateType)]
        public string Template { get; set; }

        [Column(TypeName = TaskManagementHistoryConsts.ToType)]
        public string To { get; set; }

        [Column(TypeName = TaskManagementHistoryConsts.CopyType)]
        public string Copy { get; set; }
    }
}
