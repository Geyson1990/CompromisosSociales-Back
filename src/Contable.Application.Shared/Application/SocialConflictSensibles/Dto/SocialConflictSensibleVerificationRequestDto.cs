using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictSensibles.Dto
{
    public class SocialConflictSensibleVerificationRequestDto
    {
        public string CaseNameVerificationState { get; set; }
        public bool CaseNameVerificationChange { get; set; }
        public string ProblemVerificationState { get; set; }
        public bool ProblemVerificationChange { get; set; }
    }
}
