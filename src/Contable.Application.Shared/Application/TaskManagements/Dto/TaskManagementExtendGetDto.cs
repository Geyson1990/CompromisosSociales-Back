using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.TaskManagements.Dto
{
   public  class TaskManagementExtendGetDto : EntityDto<long>
    {
        public string Description { get; set; }

        public DateTime? Deadline { get; set; }
    }
}
