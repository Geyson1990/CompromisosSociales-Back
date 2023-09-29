using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.TaskManagements.Dto
{
    public class TaskManagementPersonChangeInputDto : EntityDto
    {
        public List<TaskManagementPersonChangeDto> Changes { get; set; }
    }
}
