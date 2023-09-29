using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.TaskManagements.Dto
{
    public class TaskManagementSocialConflictDto : EntityDto
    {
        public string Code { get; set; }
        public string CaseName { get; set; }
    }
}
