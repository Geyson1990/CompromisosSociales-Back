using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilityInterventionPlanGetAllDto : EntityDto
    {
        public string Code { get; set; }
        public string CaseName { get; set; }
        public DateTime InterventionPlanTime { get; set; }
        public string Locations { get; set; }
        public string TerritorialUnits { get; set; }
        public string ConflictCode { get; set; }
        public string ConflictCaseName { get; set; }
        public ConflictSite Site { get; set; }
    }
}
