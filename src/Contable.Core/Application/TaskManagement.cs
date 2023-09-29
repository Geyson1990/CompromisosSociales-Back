using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppTaskManagement")]
    public class TaskManagement : FullAuditedEntity<long>
    {
        [Column(TypeName = TaskManagementConsts.TitleType)]
        public string Title { get; set; }

        [Column(TypeName = TaskManagementConsts.DescriptionType)]
        public string Description { get; set; }   

        [Column(TypeName = TaskManagementConsts.StartTimeType)]
        public DateTime? StartTime { get; set; }

        [Column(TypeName = TaskManagementConsts.DeadlineType)]
        public DateTime? Deadline { get; set; }

        [Column(TypeName = TaskManagementConsts.TaskStatus)]
        public TaskStatus Status { get; set; }

        [Column(TypeName = TaskManagementConsts.SendedType)]
        public bool SendedCreation { get; set; }

        [Column(TypeName = TaskManagementConsts.SendedType)]
        public bool SendedDeadline { get; set; }

        public Compromise Compromise { get; set; }
        
        public List<Comment> Comments { get; set; }
        public List<TaskManagementPerson> Persons { get; set; }
        public List<TaskManagemetExtend> ExtendDates { get; set; }    
        public List<TaskManagementHistory> Histories { get; set; }
    }
}
