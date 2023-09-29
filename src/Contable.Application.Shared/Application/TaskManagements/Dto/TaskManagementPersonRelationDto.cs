using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.TaskManagements.Dto
{
    public class TaskManagementPersonRelationDto : EntityDto
    {
        public TaskManagementPersonDto Person { get; set; }
        public bool Remove { get; set; }
    }
}
