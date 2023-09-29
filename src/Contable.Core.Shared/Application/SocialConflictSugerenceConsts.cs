using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class SocialConflictSugerenceConsts
    {
        public const int DescriptionMinLength = 0;
        public const int DescriptionMaxLength = 2000;
        public const string DescriptionType = "VARCHAR(2000)";

        public const string SocialConflictIdType = "INT";
        public const string AcceptedType = "BIT";
        public const string AcceptedUserIdType = "BIGINT";
        public const string AcceptTimeType = "DATETIME"; 
    }
}
