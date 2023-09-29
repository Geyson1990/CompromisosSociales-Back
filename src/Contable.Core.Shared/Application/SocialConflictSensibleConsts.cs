using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    public class SocialConflictSensibleConsts
    {
        public const int CodeMinLength = 0;
        public const int CodeMaxLength = 20;
        public const string CodeType = "VARCHAR(20)";

        public const int CaseNameMinLength = 0;
        public const int CaseNameMaxLength = 1000;
        public const string CaseNameType = "VARCHAR(1000)";

        public const int ProblemMinLength = 0;
        public const int ProblemMaxLength = 5000;
        public const string ProblemType = "VARCHAR(5000)";

        public const string YearType = "INT";
        public const string CountType = "INT";
        public const string GenerationType = "BIT";
        public const string AnalistIdType = "INT";
        public const string CoordinatorIdType = "INT";
        public const string ManagerIdType = "INT";
        public const string TypologyIdType = "INT";
        public const string LastConditionType = "INT";
        public const string GeographicType = "INT";
        public const string FilterType = "TEXT";
        public const string VerificationType = "BIT";
        public const string VerificationStateType = "INT";
        public const string LastSocialConflictSensibleRiskIdType = "INT";
        public const string LastSocialConflictSensibleConditionIdType = "INT";        
        public const string LastSocialConflictSensibleManagementIdType = "INT";
        public const string LastSocialConflictSensibleStateIdType = "INT";
        public const string LatitudeType = "DECIMAL(27,10)";
        public const string LongitudeType = "DECIMAL(27,10)";
        public const string PublishedType = "BIT";
    }
}
