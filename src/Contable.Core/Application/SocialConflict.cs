using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contable.Application
{
    [Table("AppSocialConflicts")]
    public class SocialConflict : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictConsts.CodeType)]
        public string Code { get; set; }

        [Column(TypeName = SocialConflictConsts.YearType)]
        public int Year { get; set; }

        [Column(TypeName = SocialConflictConsts.CountType)]
        public int Count { get; set; }

        [Column(TypeName = SocialConflictConsts.GenerationType)]
        public bool Generation { get; set; }

        [Column(TypeName = SocialConflictConsts.CaseNameType)]
        public string CaseName { get; set; }

        [Column(TypeName = SocialConflictConsts.VerificationType)]
        public bool CaseNameVerification { get; set; }

        [Column(TypeName = SocialConflictConsts.ProblemType)]
        public string Problem { get; set; }

        [Column(TypeName = SocialConflictConsts.VerificationType)]
        public bool ProblemVerification { get; set; }

        [Column(TypeName = SocialConflictConsts.DialogType)]
        public string Dialog { get; set; }

        [Column(TypeName = SocialConflictConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = SocialConflictConsts.VerificationType)]
        public bool DescriptionVerification { get; set; }

        [Column(TypeName = SocialConflictConsts.PlaintType)]
        public string Plaint { get; set; }

        [Column(TypeName = SocialConflictConsts.FactorContextType)]
        public string FactorContext { get; set; }

        [Column(TypeName = SocialConflictConsts.StrategyType)]
        public string Strategy { get; set; }

        [Column(TypeName = SocialConflictConsts.GeographicType)]
        public GeographycType GeographicType { get; set; }

        [Column(TypeName = SocialConflictConsts.GovernmentLevelType)]
        public GovernmentLevel GovernmentLevel { get; set; }

        [Column(TypeName = SocialConflictConsts.VerificationStateType)]
        public ConflictVerification Verification { get; set; }

        [Column(TypeName = SocialConflictConsts.LatitudeType)]
        public decimal Latitude { get; set; }

        [Column(TypeName = SocialConflictConsts.LongitudeType)]
        public decimal Longitude { get; set; }

        [Column(TypeName = SocialConflictConsts.PublishedType)]
        public bool Published { get; set; }

        [Column(TypeName = SocialConflictConsts.AnalistIdType)]
        [ForeignKey("Analyst")]
        public int? AnalystId { get; set; }
        public Person Analyst { get; set; }

        [Column(TypeName = SocialConflictConsts.CoordinatorIdType)]
        [ForeignKey("Coordinator")]
        public int? CoordinatorId { get; set; }
        public Person Coordinator { get; set; }

        [Column(TypeName = SocialConflictConsts.ManagerIdType)]
        [ForeignKey("Manager")]
        public int? ManagerId { get; set; }
        public Person Manager { get; set; }

        [Column(TypeName = SocialConflictConsts.SectorIdType)]
        [ForeignKey("Sector")]
        public int? SectorId { get; set; }
        public Sector Sector { get; set; }

        [Column(TypeName = SocialConflictConsts.TypologyIdType)]
        [ForeignKey("Typology")]
        public int? TypologyId { get; set; }
        public Typology Typology { get; set; }

        [Column(TypeName = SocialConflictConsts.SubTypologyIdType)]
        [ForeignKey("SubTypology")]
        public int? SubTypologyId { get; set; }
        public SubTypology SubTypology { get; set; }

        public Parameter Status { get; set; }

        [Column(TypeName = SocialConflictConsts.VerificationType)]
        public bool RiskVerification { get; set; }

        [Column(TypeName = SocialConflictConsts.VerificationType)]
        public bool ManagementVerification { get; set; }

        [Column(TypeName = SocialConflictConsts.VerificationType)]
        public bool StateVerification { get; set; }

        [Column(TypeName = SocialConflictConsts.VerificationType)]
        public bool ConditionVerification { get; set; }

        [Column(TypeName = SocialConflictConsts.LastConditionType)]
        public ConditionType LastCondition { get; set; }

        [Column(TypeName = SocialConflictConsts.LastSocialConflictConditionIdType)]
        public int? LastSocialConflictConditionId { get; set; }

        [Column(TypeName = SocialConflictConsts.LastSocialConflictRiskIdType)]
        public int? LastSocialConflictRiskId { get; set; }

        [Column(TypeName = SocialConflictConsts.LastSocialConflictStateIdType)]
        public int? LastSocialConflictStateId { get; set; }

        [Column(TypeName = SocialConflictConsts.LastSocialConflictManagementIdType)]
        public int? LastSocialConflictManagementId { get; set; }

        [Column(TypeName = SocialConflictConsts.FilterType)]
        public string Filter { get; set; }

        public List<SocialConflictNote> Notes { get; set; }
        public List<SocialConflictResource> Resources { get; set; }
        public List<Compromise> Compromises { get; set; }
        public List<SocialConflictActor> Actors { get; set; }
        public List<SocialConflictRisk> Risks { get; set; }
        public List<SocialConflictLocation> Locations { get; set; }
        public List<SocialConflictGeneralFact> GeneralFacts { get; set; }
        public List<SocialConflictSugerence> Sugerences { get; set; }
        public List<SocialConflictManagement> Managements { get; set; }
        public List<SocialConflictState> States { get; set; }
        public List<SocialConflictViolenceFact> ViolenceFacts { get; set; }
        public List<SocialConflictCondition> Conditions { get; set; }
        public List<SocialConflictUser> SocialConflictUsers { get; set; }
        public List<SocialConflictVerificationHistory> VerificationHistories { get; set; }
    }
}
