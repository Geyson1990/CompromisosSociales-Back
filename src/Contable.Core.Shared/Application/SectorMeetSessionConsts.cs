using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class SectorMeetSessionConsts
    {
        public const int LocationMinLength = 0;
        public const int LocationMaxLength = 1000;
        public const string LocationType = "VARCHAR(1000)";
        public const string LatitudeType = "VARCHAR(1000)";
        public const string LongitudeType = "VARCHAR(1000)";

        public const int SideMinLength = 0;
        public const int SideMaxLength = 1000;
        public const string SideType = "VARCHAR(1000)";

        public const string SessionTimeType = "DATETIME";
        public const string PersonTimeType = "DATETIME";
        public const string Type = "INT";
        public const string DepartmentIdType = "INT";
        public const string ProvinceIdType = "INT";
        public const string DistrictIdType = "INT";
        public const string PersonIdType = "INT";
        public const string SectorMeetIdType = "INT";        
    }
}
