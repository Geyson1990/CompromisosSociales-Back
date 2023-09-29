using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class SocialConflictAlertSectorConsts
    {
        public const string SocialConflictAlertIdType = "INT";
        public const string AlertSectorIdType = "INT";
        public const string SectorTimeType = "DATETIME"; 

        public const int DescriptionMinLength = 0;
        public const int DescriptionMaxLength = 2000;
        public const string DescriptionType = "VARCHAR(2000)";
    }
}
