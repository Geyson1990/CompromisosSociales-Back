using Abp.Application.Services.Dto;
using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictStateExportDto : EntityDto
    {
        public string CaseCode { get; set; }
        public string CaseName { get; set; }

        public string LastCaseRisk { get; set; }
        public string LastCaseRiskDescription { get; set; }
        public DateTime? LastCaseRiskTime { get; set; }

        public string LastCaseCondition {  get; set; }
        public string LastCaseConditionDescription { get; set; }
        public DateTime? LastCaseConditionTime { get; set; }

        public string TerritorialUnits { get; set; }
        public string Departments { get; set; }
        public string Provinces { get; set; }
        public string Districts { get; set; }
        public string Regions { get; set; }
        public string Ubications { get; set; }

        public string State { get; set; }
        public string StateDescription { get; set; }
        public DateTime StateTime { get; set; }
        public string StateManager { get; set; }
        public bool Verification { get; set; }
    }
}
