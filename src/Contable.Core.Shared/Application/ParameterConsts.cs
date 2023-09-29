using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class ParameterConsts
    {
        public const int ValueMinLength = 0;
        public const int ValueMaxLength = 255;
        public const string ValueType = "VARCHAR(255)";

        public const int NameMinLength = 0;
        public const int NameMaxLength = 255;
        public const string NameType = "VARCHAR(255)";

        public const int CodeMinLength = 4;
        public const int CodeMaxLength = 4;
        public const string CodeType = "VARCHAR(4)";

        public const string ParentType = "INT";
        public const string OrderType = "INT";
        public const string Type = "INT";
        public const string StepType = "INT"; 
    }
}
