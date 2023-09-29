using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.TaskManagements.Dto
{
    public class TaskManagementCommentGetDto : EntityDto<long>
    {
        public DateTime CreationTime { get; set; }
        public string Description { get; set; }
        public TaskManagementUserDto User { get; set; }
        public CommentType Type { get; set; }
    }
}
