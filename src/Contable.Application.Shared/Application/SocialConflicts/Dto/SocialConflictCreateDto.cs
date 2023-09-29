using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictCreateDto
    {
        public string CaseName { get; set; }
        public string Description { get; set; }
        public string Problem { get; set; }
        public string Dialog { get; set; }
        public string Plaint { get; set; }
        public string FactorContext { get; set; }
        public string Strategy { get; set; }
        public GeographycType GeographicType { get; set; }
        public GovernmentLevel GovernmentLevel { get; set; }
        public bool ReplaceCode { get; set; }
        public int ReplaceYear { get; set; }
        public int ReplaceCount { get; set; }
        public string CaseNameVerificationState { get; set; }
        public bool CaseNameVerificationChange { get; set; }
        public string ProblemVerificationState { get; set; }
        public bool ProblemVerificationChange { get; set; }
        public string DescriptionVerificationState { get; set; }
        public bool DescriptionVerificationChange { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool Published { get; set; }
        public SocialConflictPersonDto Analyst { get; set; }
        public SocialConflictPersonDto Coordinator { get; set; }
        public SocialConflictPersonDto Manager { get; set; }
        public SocialConflictSectorLocationDto Sector { get; set; }
        public SocialConflictTypologyDto Typology { get; set; }
        public SocialConflictSubTypologyDto SubTypology { get; set; }
        public List<SocialConflictGeneralFactDto> GeneralFacts { get; set; }
        public List<SocialConflictActorLocationDto> Actors { get; set; }
        public List<SocialConflictRiskLocationDto> Risks { get; set; }
        public List<SocialConflictLocationDto> Locations { get; set; }
        public List<SocialConflictSugerenceDto> Sugerences { get; set; }
        public List<SocialConflictSectorLocationDto> Sectors { get; set; }
        public List<SocialConflictManagementLocationDto> Managements { get; set; }
        public List<SocialConflictStateDto> States { get; set; }
        public List<SocialConflictViolenceFactDto> ViolenceFacts { get; set; }
        public List<SocialConflictConditionDto> Conditions { get; set; }
    }
}
