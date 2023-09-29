using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class SocialConflictSensibleManagementConsts
    {
        public const string SocialConflictSensibleIdType = "INT";
        public const string ManagementIdType = "INT";
        public const string ManagerIdType = "INT";

        public const int DescriptionMinLength = 0;
        public const int DescriptionMaxLength = 5000;
        public const string DescriptionType = "VARCHAR(5000)";

        public const string ManagementTimeType = "DATETIME";
        public const string CivilMenType = "INT";
        public const string CivilWomenType = "INT";
        public const string StateMenType = "INT";
        public const string StateWomenType = "INT";
        public const string CompanyMenType = "INT";
        public const string CompanyWomenType = "INT";
        public const string VerificationType = "BIT";
    }
}
