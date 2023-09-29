using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class AlertResponsibleConsts
    {
        public const int NameMinLength = 0;
        public const int NameMaxLength = 255;
        public const string NameType = "VARCHAR(255)";

        public const int ShortNameMinLength = 0;
        public const int ShortNameMaxLength = 100;
        public const string ShortNameType = "VARCHAR(100)";

        public const string TracingType = "BIT";
        public const string EnabledType = "BIT";
    }
}
