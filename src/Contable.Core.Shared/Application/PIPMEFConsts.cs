using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class PIPMEFConsts
    {
        public const int UnifiedCodeMinLength = 0;
        public const int UnifiedCodeMaxLength = 25;
        public const string UnifiedCodeType = "VARCHAR(25)";

        public const int SNIPCodeMinLength = 0;
        public const int SNIPCodeMaxLength = 25;
        public const string SNIPCodeType = "VARCHAR(25)";


        public const string ProjectNameType = "VARCHAR(500)";
        public const string ViabilityDateType = "VARCHAR(20)";
        public const string AccruedType = "NUMERIC(27,2)";
        public const string AccumulatedAccruedType = "NUMERIC(27,2)";
        public const string PIMType = "NUMERIC(27,2)";
        public const string PIAType = "NUMERIC(27,2)";
        public const string UpdatedCostType = "NUMERIC(27,2)"; 
        public const string FormulatingUnitType = "VARCHAR(200)";
        public const string ExecutingUnitType = "VARCHAR(200)";
        public const string StatusType = "VARCHAR(255)";

        public const string IsOkType = "BIT";
        public const string LastUpdateMEFType = "DATETIME";
    }
}
