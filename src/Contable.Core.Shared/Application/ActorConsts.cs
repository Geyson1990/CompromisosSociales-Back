using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class ActorConsts
    {
        public const string TypologyType = "INT";

        public const string SubTypologyType = "INT";

        public const int FullNameMinLength = 0;
        public const int FullNameMaxLength = 255;
        public const string FullNameType = "VARCHAR(255)";

        public const string ActorType = "INT";

        public const int DocumentNumberMinLength = 0;
        public const int DocumentNumberMaxLength = 25;
        public const string DocumentNumberType = "VARCHAR(25)";

        public const int WorkPositionMinLength = 0;
        public const int WorkPositionMaxLength = 255;
        public const string WorkPositionType = "VARCHAR(255)";

        public const int InstitutionMinLength = 0;
        public const int InstitutionMaxLength = 255;
        public const string InstitutionType = "VARCHAR(255)";

        public const int InstitutionAddressMinLength = 0;
        public const int InstitutionAddressMaxLength = 255;
        public const string InstitutionAddressType = "VARCHAR(255)";

        public const int PhoneNumberMinLength = 0;
        public const int PhoneNumberMaxLength = 255;
        public const string PhoneNumberType = "VARCHAR(255)";

        public const int EmailAddressMinLength = 0;
        public const int EmailAddressMaxLength = 255;
        public const string EmailAddressType = "VARCHAR(255)";

        public const int PositionMinLength = 0;
        public const int PositionMaxLength = 5000;
        public const string PositionType = "VARCHAR(5000)";

        public const int DetailsMinLength = 0;
        public const int DetailsMaxLength = 5000;
        public const string DetailsType = "VARCHAR(5000)";

        public const string ShowPoliticalType = "BIT";
        public const string EnabledType = "BIT";

    }
}