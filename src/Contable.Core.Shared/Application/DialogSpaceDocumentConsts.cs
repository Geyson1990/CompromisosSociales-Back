using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class DialogSpaceDocumentConsts
    {
        public const string DialogSpaceIdType = "INT";
        public const string DialogSpaceDocumentTypeIdType = "INT";
        public const string DialogSpaceDocumentSituationIdType = "INT";
        
        public const int DocumentMinLength = 0;
        public const int DocumentMaxLength = 32;
        public const string DocumentType = "VARCHAR(32)";

        public const int ObservationMinLength = 0;
        public const int ObservationMaxLength = 5000;
        public const string ObservationType = "VARCHAR(5000)";

        public const string DocumentTimeType = "DATETIME";
        public const string InstallationTimeType = "DATETIME";        
        public const string InstallationMaxTimeType = "DATETIME";
        public const string VigencyTimeType = "DATETIME";
        public const string HasInstallationType = "BIT";        
        public const string RangeType = "INT";
        public const string RangeSideType = "INT";
        public const string ExpositionType = "INT";
        public const string VigencyRangeSideType = "INT";
        public const string DaysType = "INT";
        public const string VigencyDaysType = "INT"; 
        public const string Type = "INT";
    }
}
