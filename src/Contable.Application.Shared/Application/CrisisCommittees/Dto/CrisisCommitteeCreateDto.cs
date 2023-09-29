using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.CrisisCommittees.Dto
{
    public class CrisisCommitteeCreateDto
    {
        public CrisisCommitteeInterventionPlanRelationDto InterventionPlan { get; set; }

        public bool ReplaceCode { get; set; }
        public int ReplaceYear { get; set; }
        public int ReplaceCount { get; set; }

        public DateTime CrisisCommitteeTime { get; set; }
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
    }
}
