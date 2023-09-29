using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class ReportConsts
    {
        public const int NameMinLength = 0;
        public const int NameMaxLength = 255;
        public const string NameType = "VARCHAR(255)";

        public const int DescriptionMinLength = 0;
        public const int DescriptionMaxLength = 500;
        public const string DescriptionType = "VARCHAR(500)";

        public const string EnabledType = "BIT";
    }
}
