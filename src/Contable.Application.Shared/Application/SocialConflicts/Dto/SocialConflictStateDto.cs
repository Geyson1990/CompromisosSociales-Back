using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictStateDto : EntityDto
    {
        public SocialConflictUserDto CreatorUser { get; set; }
        public SocialConflictPersonDto Manager { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime StateTime { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
        public string VerificationState { get; set; }
        public bool VerificationChange { get; set; }
        public bool VerificationLocation { get; set; }
        public bool Remove { get; set; }
    }
}
