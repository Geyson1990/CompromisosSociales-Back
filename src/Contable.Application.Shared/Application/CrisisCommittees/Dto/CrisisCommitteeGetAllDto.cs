using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.CrisisCommittees.Dto
{
    public class CrisisCommitteeGetAllDto : EntityDto
    {
        public CrisisCommitteeInterventionPlanRelationDto InterventionPlan { get; set; }
        public int Year { get; set; }
        public int Count { get; set; }
        public string Code { get; set; }
        public DateTime CrisisCommitteeTime { get; set; }
        public string CaseName { get; set; }
    }
}
