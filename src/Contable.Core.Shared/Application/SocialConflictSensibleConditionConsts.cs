using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class SocialConflictSensibleConditionConsts
    {
        public const int DescriptionMinLength = 0;
        public const int DescriptionMaxLength = 1000;
        public const string DescriptionType = "VARCHAR(1000)";

        public const string Type = "INT";
        public const string SocialConflictSensibleIdType = "INT";
        public const string ConditionTimeType = "DATETIME";
        public const string VerificationType = "BIT";
    }
}
