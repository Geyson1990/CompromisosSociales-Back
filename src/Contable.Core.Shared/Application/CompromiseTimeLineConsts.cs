using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class CompromiseTimeLineConsts
    {
        public const int ObservationMinLength = 0;
        public const int ObservationMaxLength = 5000;
        public const string ObservationType = "VARCHAR(5000)";

        public const string CompromiseIdType = "BIGINT";
        public const string PhaseIdType = "INT";
        public const string MilestoneIdType = "INT";
        public const string ProyectedTimeType = "DATETIME";
        public const string CompletedTimeType = "DATETIME";
    }
}
