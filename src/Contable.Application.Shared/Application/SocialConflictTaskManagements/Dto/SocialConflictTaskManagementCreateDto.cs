using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictTaskManagements.Dto
{
    public class SocialConflictTaskManagementCreateDto
    {
        public int ConflictId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? Deadline { get; set; }
        public TaskStatus Status { get; set; }
        public ConflictSite Site { get; set; }
        public List<SocialConflictTaskManagementPersonRelationDto> Persons { get; set; }
    }
}
