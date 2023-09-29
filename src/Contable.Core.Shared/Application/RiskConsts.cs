using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class RiskConsts
    {
        public const int NameMinLength = 0;
        public const int NameMaxLength = 255;
        public const string NameType = "VARCHAR(255)";

        public const int ColorMinLength = 0;
        public const int ColorMaxLength = 30;
        public const string ColorType = "VARCHAR(30)";

        public const string IndexType = "INT";
        public const string EnabledType = "BIT";
    }
}
