using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppTaskManagementPersons")]
    public class TaskManagementPerson : Entity
    {
        [Column(TypeName = TaskManagementPersonConsts.TaskManagementIdType)]
        [ForeignKey("TaskManagement")]
        public long TaskManagementId { get; set; }
        public TaskManagement TaskManagement { get; set; }

        [Column(TypeName = TaskManagementPersonConsts.PersonIdType)]
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
