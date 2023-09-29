using Abp.Application.Services.Dto;
using Contable.Application.Compromises.Dto;
using Contable.Application.ResponsibleActors.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictTaskManagements.Dto
{
    public class SocialConflictTaskManagementGetDto : EntityDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? Deadline { get; set; }
        public TaskStatus Status { get; set; } 
        public List<SocialConflictTaskManagementPersonRelationDto> Persons { get; set; }
        public List<SocialConflictTaskManagementCommentGetDto> Comments { get; set; }
        public List<SocialConflictTaskManagementResourceGetDto> Resources { get; set; }
    }
}
