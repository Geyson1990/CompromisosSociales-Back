using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class RecordConsts
    {
        public const int CodeMinLength = 0;
        public const int CodeMaxLength = 32;
        public const string CodeType = "VARCHAR(32)";

        public const int TitleMinLength = 0;
        public const int TitleMaxLength = 255;
        public const string TitleType = "VARCHAR(255)";

        public const string DatetimeType = "DATETIME";
        public const string FilterType = "TEXT";
    }
}
