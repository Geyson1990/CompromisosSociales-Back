using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class SocialConflictConsts
    {
        public const int CodeMinLength = 0;
        public const int CodeMaxLength = 20;
        public const string CodeType = "VARCHAR(20)";

        public const int CaseNameMinLength = 0;
        public const int CaseNameMaxLength = 1000;
        public const string CaseNameType = "VARCHAR(1000)";

        public const int DescriptionMinLength = 0;
        public const int DescriptionMaxLength = 5000;
        public const string DescriptionType = "VARCHAR(5000)";

        public const int ProblemMinLength = 0;
        public const int ProblemMaxLength = 5000;
        public const string ProblemType = "VARCHAR(5000)";

        public const int DialogMinLength = 0;
        public const int DialogMaxLength = 1000;
        public const string DialogType = "VARCHAR(1000)";

        public const int PlaintMinLength = 0;
        public const int PlaintMaxLength = 6000;
        public const string PlaintType = "VARCHAR(6000)";

        public const int FactorContextMinLength = 0;
        public const int FactorContextMaxLength = 5000;
        public const string FactorContextType = "VARCHAR(5000)";

        public const int StrategyMinLength = 0;
        public const int StrategyMaxLength = 5000;
        public const string StrategyType = "VARCHAR(5000)";

        public const string YearType = "INT";
        public const string CountType = "INT";
        public const string GenerationType = "BIT";
        public const string AnalistIdType = "INT";
        public const string CoordinatorIdType = "INT";
        public const string ManagerIdType = "INT";
        public const string SectorIdType = "INT";
        public const string TypologyIdType = "INT";
        public const string SubTypologyIdType = "INT";
        public const string LastConditionType = "INT"; 
        public const string GeographicType = "INT";
        public const string GovernmentLevelType = "INT";
        public const string LastSocialConflictRiskIdType = "INT";
        public const string LastSocialConflictConditionIdType = "INT";
        public const string LastSocialConflictStateIdType = "INT";
        public const string LastSocialConflictManagementIdType = "INT";        
        public const string ParameterCategoryStatus = "SC01";
        public const string VerificationType = "BIT";
        public const string VerificationStateType = "INT";
        public const string LatitudeType = "DECIMAL(27,10)";
        public const string LongitudeType = "DECIMAL(27,10)";
        public const string PublishedType = "BIT";
        public const string FilterType = "TEXT";
    }
}
