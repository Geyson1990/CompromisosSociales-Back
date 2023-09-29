using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictViolenceFactDto : EntityDto
    {
        public SocialConflictPersonDto Manager { get; set; }
        public SocialConflictFactDto Fact { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Description { get; set; }
        public string Responsible { get; set; }
        public string Actions { get; set; }
        public int InjuredMen { get; set; }
        public int InjuredWomen { get; set; }
        public int DeadMen { get; set; }
        public int DeadWomen { get; set; }
        public bool Remove { get; set; }
        public List<SocialConflictViolenceFactLocationDto> Locations { get; set; }
    }
}
