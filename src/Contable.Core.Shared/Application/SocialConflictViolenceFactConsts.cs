using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class SocialConflictViolenceFactConsts
    {
        public const int DescriptionMinLength = 0;
        public const int DescriptionMaxLength = 2000;
        public const string DescriptionType = "VARCHAR(2000)";

        public const int ResponsibleMinLength = 0;
        public const int ResponsibleMaxLength = 255;
        public const string ResponsibleType = "VARCHAR(255)";

        public const int ActionsMinLength = 0;
        public const int ActionsMaxLength = 2000;
        public const string ActionsType = "VARCHAR(2000)";

        public const string SocialConflictIdType = "INT";
        public const string ManagerIdType = "INT";
        public const string FactIdType = "INT";
        public const string StartTimeType = "DATETIME";
        public const string EndTimeType = "DATETIME";
        public const string InjuredMenType = "INT";
        public const string InjuredWomenType = "INT";
        public const string DeadMenType = "INT";
        public const string DeadWomenType = "INT";
    }
}
