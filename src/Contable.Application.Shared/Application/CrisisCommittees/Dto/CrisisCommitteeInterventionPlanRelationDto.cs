using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.CrisisCommittees.Dto
{
    public class CrisisCommitteeInterventionPlanRelationDto : EntityDto
    {
        public string Code { get; set; }
        public string CaseName { get; set; }
        public DateTime InterventionPlanTime { get; set; }
    }
}
