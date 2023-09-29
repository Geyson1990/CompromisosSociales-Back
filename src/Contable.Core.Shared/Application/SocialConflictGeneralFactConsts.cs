using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class SocialConflictGeneralFactConsts
    {
        public const int DescriptionMinLength = 0;
        public const int DescriptionMaxLength = 6000;
        public const string DescriptionType = "VARCHAR(6000)";

        public const string SocialConflictIdType = "INT";
        public const string FactTimeType = "DATETIME";
    }
}
