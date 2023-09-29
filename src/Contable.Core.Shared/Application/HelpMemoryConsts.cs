using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class HelpMemoryConsts
    {
        public const string SocialConflictIdType = "INT";
        public const string SocialConflictSensibleIdType = "INT";
        public const string DirectoryGovernmentIdType = "INT";

        public const int CodeMinLength = 0;
        public const int CodeMaxLength = 20;
        public const string CodeType = "VARCHAR(20)";

        public const int RequestMinLength = 0;
        public const int RequestMaxLength = 500;
        public const string RequestType = "VARCHAR(500)";

        public const string YearType = "INT";
        public const string CountType = "INT";
        public const string SiteType = "INT";
        public const string RequestTimeType = "DATETIME";
        public const string GenerationType = "BIT";        
    }
}
