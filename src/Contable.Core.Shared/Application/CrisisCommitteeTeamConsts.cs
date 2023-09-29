using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class CrisisCommitteeTeamConsts
    {
        public const int DocumentMinLength = 0;
        public const int DocumentMaxLength = 32;
        public const string DocumentType = "VARCHAR(32)";

        public const int NameMinLength = 0;
        public const int NameMaxLength = 255;
        public const string NameType = "VARCHAR(255)";

        public const int SurnameMinLength = 0;
        public const int SurnameMaxLength = 255;
        public const string SurnameType = "VARCHAR(255)";

        public const int SecondSurnameMinLength = 0;
        public const int SecondSurnameMaxLength = 255;
        public const string SecondSurnameType = "VARCHAR(255)";

        public const int JobMinLength = 0;
        public const int JobMaxLength = 500;
        public const string JobType = "VARCHAR(500)";

        public const string AlertResponsibleIdType = "INT";
        public const string CrisisCommitteeJobIdType = "INT";
        public const string CrisisCommitteeIdType = "INT";        
        public const string IndexType = "INT";
    }
}
