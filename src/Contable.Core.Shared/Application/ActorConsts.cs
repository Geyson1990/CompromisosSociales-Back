using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class ActorConsts
    {
        public const string TypologyIdType = "INT";

        public const string SubTypologyIdType = "INT";

        public const int FullNameMinLength = 0;
        public const int FullNameMaxLength = 255;
        public const string FullNameType = "VARCHAR(255)";

        public const string ActorTypeIdType = "INT";

        public const string ActorMovementIdType = "INT";

        public const int DocumentNumberMinLength = 0;
        public const int DocumentNumberMaxLength = 25;
        public const string DocumentNumberType = "VARCHAR(25)";

        public const int JobPositionMinLength = 0;
        public const int JobPositionMaxLength = 255;
        public const string JobPositionType = "VARCHAR(255)";

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
        public const int PositionMaxLength = 3000;
        public const string PositionType = "VARCHAR(3000)";

        public const int InterestMinLength = 0;
        public const int InterestMaxLength = 3000;
        public const string InterestType = "VARCHAR(3000)";

        public const string IsPoliticalAssociationType = "BIT";

        public const int PoliticalAssociationMinLength = 0;
        public const int PoliticalAssociationMaxLength = 255;
        public const string PoliticalAssociationType = "VARCHAR(255)";

        public const string EnabledType = "BIT";

    }
}