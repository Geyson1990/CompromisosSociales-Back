using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class ProjectRiskConsts
    {
        public const int NameMinLength = 0;
        public const int NameMaxLength = 255;
        public const string NameType = "VARCHAR(255)";

        public const int CodeMinLength = 0;
        public const int CodeMaxLength = 30;
        public const string CodeType = "VARCHAR(30)";

        public const string ProvinceIdType = "INT";
        public const string StageIdType = "INT";
        public const string EvaluatedTimeType = "DATETIME";
        public const string TotalType = "DECIMAL(27,2)";
        public const string ValueType = "DECIMAL(27,2)";
        public const string ProbabilityType = "DECIMAL(27,2)";
        public const string FixImpactRateType = "DECIMAL(27,2)";
        public const string ProbabilityWeight = "DECIMAL(27,2)";
        public const string ImpactType = "DECIMAL(27,2)";
        public const string FixProbabilityRateType = "DECIMAL(27,2)";
        public const string ImpactWeight = "DECIMAL(27,2)";
    }
}
