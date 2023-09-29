using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class DepartmentConsts
    {
        public const int NameMinLength = 0;
        public const int NameMaxLength = 255;
        public const string NameType = "VARCHAR(255)";

        public const int CodeMinLength = 2;
        public const int CodeMaxLength = 2;
        public const string CodeType = "VARCHAR(6)";

        public const string FilterType = "TEXT";
        public const string EnabledType = "BIT";
    }
}
