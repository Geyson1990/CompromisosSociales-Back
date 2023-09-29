using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictSensibles.Dto
{
    public class SocialConflictSensibleUpdateDto : EntityDto
    {        
        public string CaseName { get; set; }
        public string Problem { get; set; }
        public GeographycType GeographicType { get; set; }
        public bool ReplaceCode { get; set; }
        public int ReplaceYear { get; set; }
        public int ReplaceCount { get; set; }
        public string CaseNameVerificationState { get; set; }
        public bool CaseNameVerificationChange { get; set; }
        public string ProblemVerificationState { get; set; }
        public bool ProblemVerificationChange { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool Published { get; set; }
        public SocialConflictSensiblePersonDto Analyst { get; set; }
        public SocialConflictSensiblePersonDto Coordinator { get; set; }
        public SocialConflictSensiblePersonDto Manager { get; set; }
        public SocialConflictSensibleTypologyDto Typology { get; set; }
        public List<SocialConflictSensibleGeneralFactDto> GeneralFacts { get; set; }
        public List<SocialConflictSensibleActorLocationDto> Actors { get; set; }
        public List<SocialConflictSensibleRiskLocationDto> Risks { get; set; }
        public List<SocialConflictSensibleLocationDto> Locations { get; set; }
        public List<SocialConflictSensibleSugerenceDto> Sugerences { get; set; }
        public List<SocialConflictSensibleManagementLocationDto> Managements { get; set; }
        public List<SocialConflictSensibleStateDto> States { get; set; }
        public List<SocialConflictSensibleConditionDto> Conditions { get; set; }
        public List<SocialConflictSensibleResourceDto> Resources { get; set; }
        public List<SocialConflictSensibleNoteLocationDto> Notes { get; set; }
    }
}
