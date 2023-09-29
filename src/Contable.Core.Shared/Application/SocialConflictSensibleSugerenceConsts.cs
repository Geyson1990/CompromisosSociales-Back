using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class SocialConflictSensibleSugerenceConsts
    {
        public const int DescriptionMinLength = 0;
        public const int DescriptionMaxLength = 1000;
        public const string DescriptionType = "VARCHAR(1000)";

        public const string SocialConflictSensibleIdType = "INT";
        public const string AcceptedType = "BIT";
        public const string AcceptedUserIdType = "BIGINT";
        public const string AcceptTimeType = "DATETIME"; 
    }
}
