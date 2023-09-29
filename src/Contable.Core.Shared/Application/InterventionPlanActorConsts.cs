using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class InterventionPlanActorConsts
    {
        public const int NameMinLength = 0;
        public const int NameMaxLength = 255;
        public const string NameType = "VARCHAR(255)";

        public const int DocumentMinLength = 0;
        public const int DocumentMaxLength = 32;
        public const string DocumentType = "VARCHAR(32)";

        public const int JobMinLength = 0;
        public const int JobMaxLength = 500;
        public const string JobType = "VARCHAR(500)";

        public const int CommunityMinLength = 0;
        public const int CommunityMaxLength = 500;
        public const string CommunityType = "VARCHAR(500)";

        public const int PhoneNumberMinLength = 0;
        public const int PhoneNumberMaxLength = 255;
        public const string PhoneNumberType = "VARCHAR(255)";

        public const int EmailAddressMinLength = 0;
        public const int EmailAddressMaxLength = 255;
        public const string EmailAddressType = "VARCHAR(255)";

        public const int PoliticalAssociationMinLength = 0;
        public const int PoliticalAssociationMaxLength = 500;
        public const string PoliticalAssociationType = "VARCHAR(500)";

        public const int PositionMinLength = 0;
        public const int PositionMaxLength = 3000;
        public const string PositionType = "VARCHAR(3000)";

        public const int InterestMinLength = 0;
        public const int InterestMaxLength = 3000;
        public const string InterestType = "VARCHAR(3000)";

        public const string IsPoliticalAssociationType = "BIT";
        public const string ImportedType = "BIT";
        public const string ImportedIdType = "INT"; 
        public const string InterventionPlanIdType = "INT";
        public const string ActorTypeIdType = "INT";
        public const string ActorMovementIdType = "INT";
    }        
}