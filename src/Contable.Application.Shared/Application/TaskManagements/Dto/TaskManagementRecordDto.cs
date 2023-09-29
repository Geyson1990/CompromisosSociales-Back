using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.TaskManagements.Dto
{
    public class TaskManagementRecordDto : EntityDto
    {
        public DateTime RecordTime { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public TaskManagementSocialConflictDto SocialConflict { get; set; }
    }
}
