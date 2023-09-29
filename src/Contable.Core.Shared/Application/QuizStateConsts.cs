using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class QuizStateConsts
    {
        public const int NameMinLength = 0;
        public const int NameMaxLength = 255;
        public const string NameType = "VARCHAR(255)";

        public const int BackgroundMinLength = 0;
        public const int BackgroundMaxLength = 16;
        public const string BackgroundType = "VARCHAR(16)";

        public const int ForegroundMinLength = 0;
        public const int ForegroundMaxLength = 16;
        public const string ForegroundType = "VARCHAR(16)";

        public const string EnabledType = "BIT";     
        public const string DefaultType = "BIT";
    }
}
