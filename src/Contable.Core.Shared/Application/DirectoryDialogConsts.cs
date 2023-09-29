using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class DirectoryDialogConsts
    {
        public const int NameMinLength = 0;
        public const int NameMaxLength = 100;
        public const string NameType = "VARCHAR(100)";

        public const int FirstSurnameMinLength = 0;
        public const int FirstSurnameMaxLength = 100;
        public const string FirstSurnameType = "VARCHAR(100)";

        public const int SecondSurnameMinLength = 0;
        public const int SecondSurnameMaxLength = 100;
        public const string SecondSurnameType = "VARCHAR(100)";

        public const int JobMinLength = 0;
        public const int JobMaxLength = 300;
        public const string JobType = "VARCHAR(300)";

        public const int EmailAddressMinLength = 0;
        public const int EmailAddressMaxLength = 150;
        public const string EmailAddressType = "VARCHAR(150)";

        public const int PhoneNumberMinLength = 0;
        public const int PhoneNumberMaxLength = 300;
        public const string PhoneNumberType = "VARCHAR(300)";

        public const int MobilePhoneNumberMinLength = 0;
        public const int MobilePhoneNumberMaxLength = 300;
        public const string MobilePhoneNumberType = "VARCHAR(300)";

        public const int AdditionalInformationMinLength = 0;
        public const int AdditionalInformationMaxLength = 500;
        public const string AdditionalInformationType = "VARCHAR(500)";

        public const string EnabledType = "BIT";
        public const string DirectoryResponsibleIdType = "INT";
        public const string GovernmentIdType = "INT";
    }
}
