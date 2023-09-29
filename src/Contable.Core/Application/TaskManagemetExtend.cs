using Abp.Domain.Entities.Auditing;
using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppTaskManagemetExtend")]
    public class TaskManagemetExtend : FullAuditedEntity<long>
    {
        [Column(TypeName = TaskManagementConsts.DescriptionExtendType)]
        public string Description { get; set; }

        [Column(TypeName = TaskManagementConsts.DeadlineType)]
        public DateTime? Deadline { get; set; }

        public TaskManagement TaskManagement { get; set; }
    }
}
