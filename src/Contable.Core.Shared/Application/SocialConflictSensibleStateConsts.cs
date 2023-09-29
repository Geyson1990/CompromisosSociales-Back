using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class SocialConflictSensibleStateConsts
    {
        public const string SocialConflictSensibleIdType = "INT";
        public const string ManagerIdType = "INT";
        public const string StateTimeType = "DATETIME";

        public const int StateMinLength = 0;
        public const int StateMaxLength = 5000;
        public const string StateType = "VARCHAR(5000)";

        public const int DescriptionMinLength = 0;
        public const int DescriptionMaxLength = 5000;
        public const string DescriptionType = "VARCHAR(5000)";

        public const string VerificationType = "BIT";
    }
}
