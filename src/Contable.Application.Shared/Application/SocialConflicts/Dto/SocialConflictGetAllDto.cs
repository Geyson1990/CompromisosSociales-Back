using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictGetAllDto : EntityDto
    {
        public DateTime? LastModificationTime { get; set; }
        public DateTime CreationTime { get; set; }
        public int Year { get; set; }
        public int Count { get; set; }
        public bool Generation { get; set; }
        public string Code { get; set; }
        public string CaseName { get; set; }
        public string Description { get; set; }
        public string Dialog { get; set; }
        public string TerritorialUnits { get; set; }
        public ConflictVerification Verification { get; set; }
        public bool CaseNameVerification { get; set; }
        public bool DescriptionVerification { get; set; }
        public bool ProblemVerification { get; set; }
        public bool RiskVerification { get; set; }
        public bool ManagementVerification { get; set; }
        public bool StateVerification { get; set; }
        public bool ConditionVerification { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool Published { get; set; }
        public List<SocialConflictLocationDto> Locations { get; set; }
        public SocialConflictUserDto CreatorUser { get; set; }
        public SocialConflictUserDto EditUser { get; set; }
    }
}
