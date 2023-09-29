using Abp.Application.Services.Dto;
using System;

namespace Contable.Application.TaskManagements.Dto
{
    public class TaskManagementExtendGetAllDto : EntityDto<long>
    {
        public string Description { get; set; }

        public DateTime? Deadline { get; set; }
    }
}
