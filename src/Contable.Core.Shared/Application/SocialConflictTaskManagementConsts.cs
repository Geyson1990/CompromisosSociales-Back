using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class SocialConflictTaskManagementConsts
    {
        public const int TitleMinLength = 0;
        public const int TitleMaxLength = 100;
        public const string TitleType = "VARCHAR(100)";

        public const int DescriptionMinLength = 0;
        public const int DescriptionMaxLength = 255;
        public const string DescriptionType = "VARCHAR(255)";

        public const int DescriptionExtendMinLength = 0;
        public const int DescriptionExtendMaxLength = 255;
        public const string DescriptionExtendType = "VARCHAR(255)";

        public const string StartTimeType = "DATETIME"; 
        public const string DeadlineType = "DATETIME";
        public const string StatusType = "INT";
        public const string SiteType = "INT";
        public const string SocialConflictIdType = "INT";
        public const string SocialConflictAlertIdType = "INT";
        public const string SocialConflictSensibleIdType = "INT";
        public const string SendedType = "BIT"; 
    }
}
