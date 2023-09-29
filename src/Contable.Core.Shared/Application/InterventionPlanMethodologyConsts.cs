using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class InterventionPlanMethodologyConsts
    {
        public const string InterventionPlanIdType = "INT";
        public const string InterventionPlanOptionIdType = "INT";

        public const int DescriptionMinLength = 0;
        public const int DescriptionMaxLength = 5000;
        public const string DescriptionType = "VARCHAR(5000)";

        public const int MethodologyMinLength = 0;
        public const int MethodologyMaxLength = 5000;
        public const string MethodologyType = "VARCHAR(5000)";
    }
}
