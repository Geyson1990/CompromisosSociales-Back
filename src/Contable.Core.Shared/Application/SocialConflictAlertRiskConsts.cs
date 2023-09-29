using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class SocialConflictAlertRiskConsts
    {
        public const string SocialConflictAlertIdType = "INT";
        public const string AlertRiskIdType = "INT";

        public const int DescriptionMinLength = 0;
        public const int DescriptionMaxLength = 2000;
        public const string DescriptionType = "VARCHAR(2000)";

        public const int ObservationMinLength = 0;
        public const int ObservationMaxLength = 2000;
        public const string ObservationType = "VARCHAR(2000)";

        public const string RiskTimeType = "DATETIME";
    }
}
