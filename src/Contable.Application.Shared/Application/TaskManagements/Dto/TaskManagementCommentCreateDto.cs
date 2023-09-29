using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.TaskManagements.Dto
{
    public class TaskManagementCommentCreateDto
    {
        public string Description { get; set; }
        public EntityDto<long> TaskManagement { get; set; }
    }
}
