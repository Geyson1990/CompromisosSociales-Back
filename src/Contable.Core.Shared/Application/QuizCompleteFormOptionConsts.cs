using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class QuizCompleteFormOptionConsts
    {
        public const int NameMinLength = 0;
        public const int NameMaxLength = 2500;
        public const string NameType = "VARCHAR(2500)";

        public const int DescriptioMinLength = 0;
        public const int DescriptioMaxLength = 3000;
        public const string DescriptionType = "VARCHAR(3000)";

        public const string IndexType = "INT";
        public const string ExtraType = "INT";        
        public const string QuizCompleteFormIdType = "INT";
        public const string QuizOptionReferenceId = "INT";        
    }
}
