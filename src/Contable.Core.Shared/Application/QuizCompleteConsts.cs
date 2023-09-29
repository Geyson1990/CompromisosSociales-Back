using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class QuizCompleteConsts
    {
        public const int NameMinLength = 0;
        public const int NameMaxLength = 256;
        public const string NameType = "VARCHAR(256)";

        public const int SurnameMinLength = 0;
        public const int SurnameMaxLength = 256;
        public const string SurnameType = "VARCHAR(256)";

        public const int SecondSurnameMinLength = 0;
        public const int SecondSurnameMaxLength = 256;
        public const string SecondSurnameType = "VARCHAR(256)";

        public const int EmailAddressMinLength = 0;
        public const int EmailAddressMaxLength = 256;
        public const string EmailAddressType = "VARCHAR(256)";

        public const string Type = "INT";
        public const string QuizStateIdType = "INT"; 
        public const string AdminitrativeIdType = "BIGINT"; 
    }
}
