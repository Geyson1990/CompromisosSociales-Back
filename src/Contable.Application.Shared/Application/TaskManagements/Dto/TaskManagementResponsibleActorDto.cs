using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.TaskManagements.Dto
{
    public class TaskManagementResponsibleActorDto : EntityDto
    {
        public string Name { get; set; }
        public ResponsibleActorType Type { get; set; }
        public List<TaskManagementResponsibleSubActorDto> ResponsibleSubActors { get; set; }
    }
}
