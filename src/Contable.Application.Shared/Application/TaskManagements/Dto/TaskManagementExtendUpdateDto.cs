using Abp.Application.Services.Dto;
using Contable.Application.Compromises.Dto;
using Contable.Application.ResponsibleActors.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.TaskManagements.Dto
{
    public class TaskManagementExtendUpdateDto : EntityDto<long>
    {
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public TaskManagementGetDto TaskManagement { get; set; }
    }
}
