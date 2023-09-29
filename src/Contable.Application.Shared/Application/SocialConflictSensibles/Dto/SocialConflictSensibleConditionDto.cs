using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictSensibles.Dto
{
    public class SocialConflictSensibleConditionDto : EntityDto
    {
        public DateTime? CreationTime { get; set; }
        public DateTime ConditionTime { get; set; }
        public ConditionType Type { get; set; }
        public string Description { get; set; }
        public string VerificationState { get; set; }
        public bool VerificationChange { get; set; }
        public bool VerificationLocation { get; set; }
        public bool Remove { get; set; }
    }
}
