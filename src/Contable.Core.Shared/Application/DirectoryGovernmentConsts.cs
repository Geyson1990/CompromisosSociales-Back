using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class DirectoryGovernmentConsts
    {
        public const int NameMinLength = 0;
        public const int NameMaxLength = 250;
        public const string NameType = "VARCHAR(250)";

        public const int ShortNameMinLength = 0;
        public const int ShortNameMaxLength = 100;
        public const string ShortNameType = "VARCHAR(100)";

        public const int AliasMinLength = 0;
        public const int AliasMaxLength = 255;
        public const string AliasType = "VARCHAR(255)";

        public const int AddressMinLength = 0;
        public const int AddressMaxLength = 500;
        public const string AddressType = "VARCHAR(500)";

        public const int PhoneNumberMinLength = 0;
        public const int PhoneNumberMaxLength = 300;
        public const string PhoneNumberType = "VARCHAR(300)";

        public const int UrlMinLength = 0;
        public const int UrlMaxLength = 300;
        public const string UrlType = "VARCHAR(300)";

        public const int AdditionalInformationMinLength = 0;
        public const int AdditionalInformationMaxLength = 500;
        public const string AdditionalInformationType = "VARCHAR(500)";

        public const string EnabledType = "BIT";
        public const string SectorIdType = "INT";
        public const string DistrictIdType = "INT";
        public const string DirectoryGovernmentIdType = "INT";
    }
}
