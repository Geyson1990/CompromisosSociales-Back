using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictTaskManagements.Dto
{
    public class SocialConflictTaskManagementUpdateDto : EntityDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? Deadline { get; set; }
        public TaskStatus Status { get; set; }
        public List<SocialConflictTaskManagementPersonRelationDto> Persons { get; set; }
        public List<SocialConflictTaskManagementResourceGetDto> Resources { get; set; }
    }
}
