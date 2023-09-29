using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.TaskManagements.Dto
{
    public class TaskManagementEmailConfigurationDto : EntityDto<long>
    {
        public string Subject { get; set; }
        public string Template { get; set; }
    }
}