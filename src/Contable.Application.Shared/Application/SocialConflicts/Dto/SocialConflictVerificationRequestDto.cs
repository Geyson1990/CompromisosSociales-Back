using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictVerificationRequestDto
    {
        public string CaseNameVerificationState { get; set; }
        public bool CaseNameVerificationChange { get; set; }
        public string ProblemVerificationState { get; set; }
        public bool ProblemVerificationChange { get; set; }
        public string DescriptionVerificationState { get; set; }
        public bool DescriptionVerificationChange { get; set; }
    }
}
