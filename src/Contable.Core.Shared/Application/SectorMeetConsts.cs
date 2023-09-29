using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class SectorMeetConsts
    {
        public const int CodeMinLength = 0;
        public const int CodeMaxLength = 20;
        public const string CodeType = "VARCHAR(20)";

        public const int MeetNameMinLength = 0;
        public const int MeetNameMaxLength = 1000;
        public const string MeetNameType = "VARCHAR(1000)";

        public const string YearType = "INT";
        public const string CountType = "INT";
        public const string GenerationType = "BIT";
        public const string TerritorialUnitIdType = "INT";
        public const string SocialConflictIdType = "INT";
    }
}
