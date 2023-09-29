using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.InterventionPlans.Dto
{
    public class InterventionPlanGetAllDto : EntityDto
    {
        public int Year { get; set; }
        public int Count { get; set; }
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
