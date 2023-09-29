using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class OrderConsts
    {
        public const int CodeMinLength = 0;
        public const int CodeMaxLength = 20;
        public const string CodeType = "VARCHAR(20)";

        public const int NameMinLength = 0;
        public const int NameMaxLength = 100;
        public const string NameType = "VARCHAR(100)";

        public const int DescriptionMinLength = 0;
        public const int DescriptionMaxLength = 5000;
        public const string DescriptionType = "VARCHAR(5000)";

        public const string Type = "INT";

        public const int ObservationMinLength = 0;
        public const int ObservationMaxLength = 5000;
        public const string ObservationType = "VARCHAR(5000)";

        public const int DocumentMinLength = 0;
        public const int DocumentMaxLength = 100;
        public const string DocumentType = "VARCHAR(100)";

        public const int ResponsibleMinLength = 0;
        public const int ResponsibleMaxLength = 100;
        public const string ResponsibleType = "VARCHAR(100)";

        public const string DatetimeType = "DATETIME";
    }
}
