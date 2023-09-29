using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.TaskManagements.Dto
{
    public class TaskManagementExtendCreateDto
    {
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public TaskManagementGetDto TaskManagement { get; set; }
    }
}
