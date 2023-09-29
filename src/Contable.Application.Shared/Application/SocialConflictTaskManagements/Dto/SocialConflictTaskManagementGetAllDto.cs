using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictTaskManagements.Dto
{
    public class SocialConflictTaskManagementGetAllDto : EntityDto
    {
        public int ConflictId { get; set; }
        public string ConflictCode { get; set; }
        public string ConflictName { get; set; }
        public string ConflictTerritorialUnits { get; set; }
        public ConflictSite ConflictSite { get; set; }

        public string Title { get; set; }
        public DateTime CreationTime { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? Deadline { get; set; }
    }
}
