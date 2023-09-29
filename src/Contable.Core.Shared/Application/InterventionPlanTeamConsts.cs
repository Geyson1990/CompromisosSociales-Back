using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class InterventionPlanTeamConsts
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

        public const int RoleMinLength = 0;
        public const int RoleMaxLength = 500;
        public const string RoleType = "VARCHAR(500)";

        public const int EntityMinLength = 0;
        public const int EntityMaxLength = 5000;
        public const string EntityType = "VARCHAR(5000)";

        public const string InterventionPlanEntityIdType = "INT";
        public const string AlertResponsibleIdType = "INT";
        public const string DirectoryGovernmentIdType = "INT";
        public const string InterventionPlanRoleIdType = "INT";
        public const string InterventionPlanIdType = "INT"; 
    }
}
