using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class SocialConflictAlertConsts
    {
        public const int CodeMinLength = 0;
        public const int CodeMaxLength = 255;
        public const string CodeType = "VARCHAR(255)";

        public const int DescriptionMinLength = 0;
        public const int DescriptionMaxLength = 1000;
        public const string DescriptionType = "VARCHAR(1000)";

        public const int InformationMinLength = 0;
        public const int InformationMaxLength = 6000;
        public const string InformationType = "VARCHAR(6000)";

        public const int DemandMinLength = 0;
        public const int DemandMaxLength = 5000;
        public const string DemandType = "VARCHAR(5000)";

        public const int AditionalInformationMinLength = 0;
        public const int AditionalInformationMaxLength = 3000;
        public const string AditionalInformationType = "VARCHAR(3000)";

        public const int SourceMinLength = 0;
        public const int SourceMaxLength = 1000;
        public const string SourceType = "VARCHAR(1000)";

        public const int SourceTypeMinLength = 0;
        public const int SourceTypeMaxLength = 1000;
        public const string SourceTypeType = "VARCHAR(1000)";

        public const int LinkMinLength = 0;
        public const int LinkMaxLength = 1000;
        public const string LinkType = "VARCHAR(1000)";

        public const int RecommendationsTypeMinLength = 0;
        public const int RecommendationsTypeMaxLength = 3000;
        public const string RecommendationsType = "VARCHAR(3000)";

        public const int ActionsMinLength = 0;
        public const int ActionsMaxLength = 3000;
        public const string ActionsType = "VARCHAR(3000)";

        public const string YearType = "INT";
        public const string CountType = "INT";
        public const string GenerationType = "BIT";
        public const string AlterTimeType = "DATETIME";
        public const string TerritorialUnitIdType = "INT";
        public const string SocialConflictIdType = "INT";
        public const string AlertDemandIdType = "INT";
        public const string TypologyIdType = "INT";
        public const string SubTypologyIdType = "INT";
        public const string AlertResponsibleIdType = "INT";
        public const string AnalystIdType = "INT";
        public const string ManagerIdType = "INT";
        public const string CoordinatorIdType = "INT";
        public const string LastAlertRiskIdType = "INT";
        public const string LastSealIdType = "INT";
        public const string LastStateIdType = "INT";        
        public const string LastSectorIdType = "INT";
        public const string LatitudeType = "DECIMAL(27,10)";
        public const string LongitudeType = "DECIMAL(27,10)";
        public const string PublishedType = "BIT";

    }
}
