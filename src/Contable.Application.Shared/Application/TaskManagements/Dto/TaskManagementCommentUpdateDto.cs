using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.TaskManagements.Dto
{
    public class TaskManagementCommentUpdateDto : EntityDto<long>
    {
        public string Description { get; set; } 
        public TaskManagementGetDto TaskManagement { get; set; }
    }
}
