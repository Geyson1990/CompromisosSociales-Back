using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class SocialConflictSensibleRiskConsts
    {
        public const int DescriptionMinLength = 0;
        public const int DescriptionMaxLength = 2000;
        public const string DescriptionType = "VARCHAR(2000)";

        public const string SocialConflictSensibleIdType = "INT";
        public const string RiskIdType = "INT";
        public const string RiskTimeType = "DATETIME";
        public const string VerificationType = "BIT";
    }
}
