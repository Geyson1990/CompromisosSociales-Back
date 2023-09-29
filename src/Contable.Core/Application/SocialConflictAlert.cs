using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictAlerts")]
    public class SocialConflictAlert : FullAuditedEntity
    {
        #region General Information

        [Column(TypeName = SocialConflictAlertConsts.SocialConflictIdType)]
        public int? SocialConflictId { get; set; }
        public SocialConflict SocialConflict { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.YearType)]
        public int Year { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.CountType)]
        public int Count { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.GenerationType)]
        public bool Generation { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.CodeType)]
        public string Code { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.InformationType)]
        public string Information { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.AlterTimeType)]
        public DateTime AlertTime { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.LatitudeType)]
        public decimal Latitude { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.LongitudeType)]
        public decimal Longitude { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.PublishedType)]
        public bool Published { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.TerritorialUnitIdType)]
        public int? TerritorialUnitId { get; set; }
        public TerritorialUnit TerritorialUnit { get; set; }

        #endregion

        #region Aditional Information

        [Column(TypeName = SocialConflictAlertConsts.DemandType)]
        public string Demand { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.AlertDemandIdType)]
        [ForeignKey("AlertDemand")]
        public int? AlertDemandId { get; set; }
        public AlertDemand AlertDemand { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.TypologyIdType)]
        [ForeignKey("Typology")]
        public int? TypologyId { get; set; }
        public Typology Typology { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.SubTypologyIdType)]
        [ForeignKey("SubTypology")]
        public int? SubTypologyId { get; set; }
        public SubTypology SubTypology { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.AlertResponsibleIdType)]
        [ForeignKey("AlertResponsible")]
        public int? AlertResponsibleId { get; set; }
        public AlertResponsible AlertResponsible { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.AnalystIdType)]
        [ForeignKey("Analyst")]
        public int? AnalystId { get; set; }
        public Person Analyst { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.ManagerIdType)]
        [ForeignKey("Manager")]
        public int? ManagerId { get; set; }
        public Person Manager { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.CoordinatorIdType)]
        [ForeignKey("Coordinator")]
        public int? CoordinatorId { get; set; }
        public Person Coordinator { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.AditionalInformationType)]
        public string AditionalInformation { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.SourceType)]
        public string Source { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.SourceTypeType)]
        public string SourceType { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.LinkType)]
        public string Link { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.RecommendationsType)]
        public string Recommendations { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.ActionsType)]
        public string Actions { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.LastAlertRiskIdType)]
        public int? LastAlertRiskId { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.LastSealIdType)]
        public int? LastSealId { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.LastStateIdType)]
        public int? LastStateId { get; set; }

        [Column(TypeName = SocialConflictAlertConsts.LastSectorIdType)]
        public int? LastSectorId { get; set; }

        #endregion

        public List<SocialConflictActor> Actors { get; set; }
        public List<SocialConflictAlertLocation> Locations { get; set; }
        public List<SocialConflictAlertRisk> Risks { get; set; }
        public List<SocialConflictAlertSector> Sectors { get; set; }
        public List<SocialConflictAlertState> States { get; set; }
        public List<SocialConflictAlertSeal> Seals { get; set; }
        public List<SocialConflictAlertResource> Resources { get; set; }
        public List<SocialConflictAlertHistory> Histories { get; set; }
    }
}
