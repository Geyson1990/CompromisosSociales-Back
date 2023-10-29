using Abp.Application.Services.Dto;
using Contable.Application.Parameters.Dto;
using Contable.Application.Records.Dto;
using System;
using System.Collections.Generic;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictGetDto : EntityDto
    {
        public DateTime? LastModificationTime { get; set; }
        public DateTime? CreationTime { get; set; }
        public string Code { get; set; }
        public string CaseName { get; set; }
        public string Description { get; set; }
        public string Problem { get; set; }
        public string Dialog { get; set; }
        public string Plaint { get; set; }
        public string FactorContext { get; set; }
        public string Strategy { get; set; }
        public GeographycType GeographicType { get; set; }
        public GovernmentLevel GovernmentLevel { get; set; }
        public string CaseNameVerificationState { get; set; }
        public bool CaseNameVerification { get; set; }
        public string DescriptionVerificationState { get; set; }
        public bool DescriptionVerification { get; set; }
        public string ProblemVerificationState { get; set; }
        public bool ProblemVerification { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool Published { get; set; }
        public ParameterDto Status { get; set; }
        public SocialConflictPersonDto Analyst { get; set; }
        public SocialConflictPersonDto Coordinator { get; set; }
        public SocialConflictPersonDto Manager { get; set; }
        public SocialConflictSectorLocationDto Sector { get; set; }
        public SocialConflictTypologyDto Typology { get; set; }
        public SocialConflictSubTypologyDto SubTypology { get; set; }
        public List<SocialConflictNoteLocationDto> Notes { get; set; }
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
        public List<SocialConflictResourceDto> Resources { get; set; }
        public List<RecordGetDto> Records { get; set; }
        public SocialConflictUserDto CreatorUser { get; set; }
        public SocialConflictUserDto EditUser { get; set; }
    }

}
