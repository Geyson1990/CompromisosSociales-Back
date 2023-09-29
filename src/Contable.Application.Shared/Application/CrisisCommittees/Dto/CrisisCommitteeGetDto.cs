using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.CrisisCommittees.Dto
{
    public class CrisisCommitteeGetDto : EntityDto
    {
        public CrisisCommitteeInterventionPlanRelationDto InterventionPlan { get; set; }
        public int Year { get; set; }
        public int Count { get; set; }
        public string Code { get; set; }
        public DateTime CrisisCommitteeTime { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string CaseName { get; set; }

        public CrisisCommitteePersonRelationDto Person { get; set; }

        public List<CrisisCommitteeTeamRelationDto> Teams { get; set; }
        public List<CrisisCommitteePlanRelationDto> Plans { get; set; }
        public List<CrisisCommitteeActionRelationDto> Actions { get; set; }
        public List<CrisisCommitteeMessageRelationDto> Messages { get; set; }
        public List<CrisisCommitteeChannelRelationDto> Channels { get; set; }
        public List<CrisisCommitteeSectorRelationDto> Sectors { get; set; }
        public List<CrisisCommitteeTaskRelationDto> Tasks { get; set; }
        public List<CrisisCommitteeAgreementRelationDto> Agreements { get; set; }
        public CrisisCommitteeUserDto CreatorUser { get; set; }
        public CrisisCommitteeUserDto EditUser { get; set; }
    }
}
