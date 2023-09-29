using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class DirectoryIndustryConsts
    {
        public const int NameMinLength = 0;
        public const int NameMaxLength = 150;
        public const string NameType = "VARCHAR(150)";

        public const int PhoneNumberMinLength = 0;
        public const int PhoneNumberMaxLength = 300;
        public const string PhoneNumberType = "VARCHAR(300)";

        public const int EmailAddressMinLength = 0;
        public const int EmailAddressMaxLength = 150;
        public const string EmailAddressType = "VARCHAR(150)";

        public const int UrlMinLength = 0;
        public const int UrlMaxLength = 150;
        public const string UrlType = "VARCHAR(150)";

        public const int AddressMinLength = 0;
        public const int AddressMaxLength = 500;
        public const string AddressType = "VARCHAR(500)";

        public const int AdditionalInformationMinLength = 0;
        public const int AdditionalInformationMaxLength = 500;
        public const string AdditionalInformationType = "VARCHAR(500)";

        public const string EnabledType = "BIT";
        public const string DistrictIdType = "INT";
        public const string DirectorySectorIdType = "INT";
    }
}
