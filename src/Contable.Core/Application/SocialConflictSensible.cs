using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contable.Application
{
    [Table("AppSocialConflictSensibles")]
    public class SocialConflictSensible : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictSensibleConsts.CodeType)]
        public string Code { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.YearType)]
        public int Year { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.CountType)]
        public int Count { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.GenerationType)]
        public bool Generation { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.CaseNameType)]
        public string CaseName { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.VerificationType)]
        public bool CaseNameVerification { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.ProblemType)]
        public string Problem { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.VerificationType)]
        public bool ProblemVerification { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.GeographicType)]
        public GeographycType GeographicType { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.VerificationStateType)]
        public ConflictVerification Verification { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.LatitudeType)]
        public decimal Latitude { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.LongitudeType)]
        public decimal Longitude { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.PublishedType)]
        public bool Published { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.AnalistIdType)]
        [ForeignKey("Analyst")]
        public int? AnalystId { get; set; }
        public Person Analyst { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.CoordinatorIdType)]
        [ForeignKey("Coordinator")]
        public int? CoordinatorId { get; set; }
        public Person Coordinator { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.ManagerIdType)]
        [ForeignKey("Manager")]
        public int? ManagerId { get; set; }
        public Person Manager { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.TypologyIdType)]
        [ForeignKey("Typology")]
        public int? TypologyId { get; set; }
        public Typology Typology { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.VerificationType)]
        public bool RiskVerification { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.VerificationType)]
        public bool ManagementVerification { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.VerificationType)]
        public bool StateVerification { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.VerificationType)]
        public bool ConditionVerification { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.LastConditionType)]
        public ConditionType LastCondition { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.LastSocialConflictSensibleConditionIdType)]
        public int? LastSocialConflictSensibleConditionId { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.LastSocialConflictSensibleRiskIdType)]
        public int? LastSocialConflictSensibleRiskId { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.LastSocialConflictSensibleManagementIdType)]
        public int? LastSocialConflictSensibleManagementId { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.LastSocialConflictSensibleStateIdType)]
        public int? LastSocialConflictSensibleStateId { get; set; }

        [Column(TypeName = SocialConflictSensibleConsts.FilterType)]
        public string Filter { get; set; }

        public List<SocialConflictActor> Actors { get; set; }
        public List<SocialConflictSensibleRisk> Risks { get; set; }
        public List<SocialConflictSensibleLocation> Locations { get; set; }
        public List<SocialConflictSensibleGeneralFact> GeneralFacts { get; set; }
        public List<SocialConflictSensibleSugerence> Sugerences { get; set; }
        public List<SocialConflictSensibleManagement> Managements { get; set; }
        public List<SocialConflictSensibleState> States { get; set; }
        public List<SocialConflictSensibleCondition> Conditions { get; set; }
        public List<SocialConflictSensibleResource> Resources { get; set; }
        public List<SocialConflictSensibleNote> Notes { get; set; }
        public List<SocialConflictSensibleVerificationHistory> VerificationHistories { get; set; }
    }
}
