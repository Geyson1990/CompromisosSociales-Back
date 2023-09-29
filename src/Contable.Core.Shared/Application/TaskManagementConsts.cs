using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class TaskManagementConsts
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
        public const string TaskStatus = "INT";
        public const string SendedType = "BIT";
    }
}
