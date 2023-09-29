using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class PersonConsts
    {
        public const int DocumentMinLength = 8;
        public const int DocumentMaxLength = 8;
        public const string DocumentType = "VARCHAR(25)";

        public const int NameMinLength = 0;
        public const int NameMaxLength = 765;
        public const string NameType = "VARCHAR(765)";

        public const int NamesMinLength = 0;
        public const int NamesMaxLength = 255;
        public const string NamesType = "VARCHAR(255)";

        public const int SurnameMinLength = 0;
        public const int SurnameMaxLength = 255;
        public const string SurnameType = "VARCHAR(255)";

        public const int Surname2MinLength = 0;
        public const int Surname2MaxLength = 255;
        public const string Surname2Type = "VARCHAR(255)";

        public const int EmailAddressMinLength = 0;
        public const int EmailAddressaxLength = 256;
        public const string EmailAddressType = "VARCHAR(256)";
        
        public const string PersonType = "INT"; 
        public const string EnabledType = "BIT";
        public const string ParentIdType = "INT";
        public const string TerritorialUnitIdType = "INT"; 
    }
}
