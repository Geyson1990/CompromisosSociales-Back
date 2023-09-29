using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class LevelConsts
    {
        public const int NameMinLength = 0;
        public const int NameMaxLength = 500;
        public const string NameType = "VARCHAR(500)";

        public const int ColorMinLength = 0;
        public const int ColorMaxLength = 10;
        public const string ColorType = "VARCHAR(10)";

        public const string MaxType = "DECIMAL(27,2)";
        public const string MinType = "DECIMAL(27,2)";
        public const string IndexType = "INT"; 
        public const string Type = "INT";
    }
}
