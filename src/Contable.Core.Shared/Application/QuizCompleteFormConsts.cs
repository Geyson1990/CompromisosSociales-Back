using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class QuizCompleteFormConsts
    {
        public const int NameMinLength = 0;
        public const int NameMaxLength = 2500;
        public const string NameType = "VARCHAR(2500)";

        public const string IndexType = "INT";
        public const string RequiredType = "BIT";
        public const string SelectedOptionIdType = "INT";        
        public const string FormReferenceIdType = "INT";
        public const string QuizCompleteIdType = "INT";
    }
}
