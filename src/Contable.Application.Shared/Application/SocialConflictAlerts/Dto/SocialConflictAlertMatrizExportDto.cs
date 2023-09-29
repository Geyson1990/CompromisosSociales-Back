using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictAlerts.Dto
{
    public class SocialConflictAlertMatrizExportDto
    {
        public string AlertCode { get; set; }
        public string AlertName { get; set; }
        public DateTime AlertTime { get; set; }

        public string CaseName { get; set; }

        public string Information { get; set; }
        public string TerritorialUnit { get; set; }
        public string Departments { get; set; }
        public string Provinces { get; set; }
        public string Districts { get; set; }
        public string Regions { get; set; }
        public string Ubications { get; set; }

        public string DemandType { get; set; }
        public string Demand { get; set; }
        public string TypologyDescription { get; set; }
        public string SubTypologyDescription { get; set; }
        public string AlertResponsible { get; set; }
        public string CoordinatorName { get; set; }
        public string ManagerName { get; set; }
        public string AnalystName { get; set; }
        public string Actions { get; set; }
        public string Recommendations { get; set; }
        public string AditionalInformation { get; set; }
        public string Source { get; set; }
        public string SourceType { get; set; }
        public string Link { get; set; }

        public string ActorDescriptions { get; set; }
        public string ActorPositions { get; set; }
        public string ActorInterests { get; set; }

        public string Attention { get; set; }
        public string AttentionDescription { get; set; }
        public DateTime? AttentionTime { get; set; }

        public string LastStateDescription { get; set; }
        public DateTime? LastStateTime { get; set; }

        public string LastSeal { get; set; }
        public string LastSealDescription { get; set; }
        public DateTime? LastSealTime { get; set; }

        public string LastCaseRisk { get; set; }
        public string LastCaseRiskDescription { get; set; }
        public DateTime? LastCaseRiskTime { get; set; }

        public string CreatorUser { get; set; }
        public DateTime CreationTime { get; set; }
        public string LastModificationUser { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
