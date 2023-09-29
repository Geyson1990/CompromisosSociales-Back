using Abp.Application.Services.Dto;
using Contable.Application.ResponsibleActors.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.TaskManagements.Dto
{
    public class TaskManagementGetAllDto : EntityDto<long>
    {
        public string Title { get; set; }
        public DateTime CreationTime { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? Deadline { get; set; }
        public string Responsible { get; set; }

        public string CaseCode { get; set; }
        public string RecordCode { get; set; }
        public string CompromiseCode { get; set; }
        public string CompromiseName { get; set; }
        public long CompromiseId { get; set; }
        public string Advance { get; set; }
        public string Alert { get; set; }
    }
}
