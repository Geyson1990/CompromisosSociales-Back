using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using Contable.Application.External.Dto;
using System;

namespace Contable.Application.TaskManagements.Dto
{
    public class TaskManagementGetMatrixExcelDto : EntityDto<long>, IHasCreationTime
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Responsible { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? Deadline { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreationTime { get; set; }
                
        public string CompromiseCode { get; set; }
        public string CompromiseName { get; set; }
        public PIPMEFDto PIPMEF { get; set; }
        public string Alert { get; set; }
        public string Advance { get; set; }
    }
}
