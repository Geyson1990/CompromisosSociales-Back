using Abp.Application.Services.Dto;
using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application.SocialConflictSensibles.Dto
{
    public class SocialConflictSensibleManagementExportDto : EntityDto
    {
        public string CaseCode { get; set; }
        public string CaseName { get; set; }

        public string LastCaseRisk { get; set; }
        public DateTime? LastCaseRiskTime { get; set; }
        public string LastCaseRiskDescription { get; set; }

        public string LastCaseCondition { get; set; }
        public DateTime? LastCaseConditionTime { get; set; }
        public string LastCaseConditionDescription { get; set; }

        public string TerritorialUnits { get; set; }
        public string Departments { get; set; }
        public string Provinces { get; set; }
        public string Districts { get; set; }
        public string Regions { get; set; }
        public string Ubications { get; set; }

        public DateTime ManagementTime { get; set; }
        public string Management { get; set; }
        public string ManagementDescription { get; set; }
        public string ManagementManager { get; set; }
        public int? CivilMen { get; set; }
        public int? CivilWomen { get; set; }
        public int? StateMen { get; set; }
        public int? StateWomen { get; set; }
        public int? CompanyMen { get; set; }
        public int? CompanyWomen { get; set; }
        public bool Verification { get; set; }
    }
}
