using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictMatrizExportDto : EntityDto
    {
        public string Code { get; set; }
        public string CaseName { get; set; }
        public string Description { get; set; }
        public string Problem { get; set; }
        public string TerritorialUnits { get; set; }
        public string Departments { get; set; }
        public string Provinces { get; set; }
        public string Districts { get; set; }
        public string Regions { get; set; }
        public string Ubications { get; set; }
        public GeographycType GeographicType { get; set; }
        public string CoordinatorName { get; set; }
        public string ManagerName { get; set; }
        public string AnalystName { get; set; }
        public string Dialog { get; set; }
        public string TypologyDescription { get; set; }
        public string SubTypologyDescription { get; set; }
        public string SectorDescription { get; set; }
        public GovernmentLevel GovernmentLevel { get; set; }
        public string ActorDescriptions { get; set; }
        public string ActorPositions { get; set; }
        public string ActorInterests { get; set; }

        public string LastRisk { get; set; }
        public DateTime? LastRiskTime { get; set; }
        public string LastRiskDescription { get; set; }

        public string LastCondition { get; set; }
        public DateTime? LastConditionTime { get; set; }
        public string LastConditionDescription { get; set; }

        public string LastManagement { get; set; }
        public DateTime? LastManagementTime { get; set; }
        public string LastManagementDescription { get; set; }
        public string LastManagementManager { get; set; }

        public string LastState { get; set; }
        public DateTime? LastStateTime { get; set; }
        public string LastStateDescription { get; set; }
        public string LastStateManager { get; set; }

        public bool CaseNameVerification { get; set; }
        public bool ProblemVerification { get; set; }
        public bool DescriptionVerification { get; set; }
        public bool RiskVerification { get; set; }
        public bool ManagementVerification { get; set; }
        public bool StateVerification { get; set; }
        public bool ConditionVerification { get; set; }

        public string CreatorUser { get; set; }
        public DateTime CreationTime { get; set; }
        public string LastModificationUser { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
