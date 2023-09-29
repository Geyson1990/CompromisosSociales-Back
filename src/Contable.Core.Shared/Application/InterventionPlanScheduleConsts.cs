using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class InterventionPlanScheduleConsts
    {
        public const int ScheduleMinLength = 0;
        public const int ScheduleMaxLength = 5000;
        public const string ScheduleType = "VARCHAR(5000)";

        public const int ActivityMinLength = 0;
        public const int ActivityMaxLength = 5000;
        public const string ActivityType = "VARCHAR(5000)";

        public const int EntityMinLength = 0;
        public const int EntityMaxLength = 5000;
        public const string EntityType = "VARCHAR(5000)";

        public const int ProductMinLength = 0;
        public const int ProductMaxLength = 5000;
        public const string ProductType = "VARCHAR(5000)";

        public const string InterventionPlanIdType = "INT";
        public const string ScheduleTimeType = "DATETIME";
        public const string InterventionPlanActivityIdType = "INT";
        public const string InterventionPlanEntityIdType = "INT";
        public const string AlertResponsibleIdType = "INT";
        public const string DirectoryGovernmentIdType = "INT";
        public const string InterventionPlanMethodologyIdType = "INT";
    }
}
