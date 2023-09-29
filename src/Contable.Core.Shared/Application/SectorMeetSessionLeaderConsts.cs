using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class SectorMeetSessionLeaderConsts
    {
        public const int EntityMinLength = 0;
        public const int EntityMaxLength = 5000;
        public const string EntityType = "VARCHAR(5000)";

        public const int RoleMinLength = 0;
        public const int RoleMaxLength = 5000;
        public const string RoleType = "VARCHAR(5000)";

        public const string Type = "INT";
        public const string IndexType = "INT";
        public const string DirectoryGovernmentIdType = "INT";
        public const string DirectoryIndustryIdType = "INT";
        public const string SectorMeetSessionIdType = "INT";        
    }
}
