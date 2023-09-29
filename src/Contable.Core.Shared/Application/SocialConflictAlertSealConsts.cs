using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class SocialConflictAlertSealConsts
    {
        public const string SocialConflictAlertIdType = "INT";
        public const string AlertSealIdType = "INT";
        public const string SealTimeType = "DATETIME"; 

        public const int DescriptionMinLength = 0;
        public const int DescriptionMaxLength = 2000;
        public const string DescriptionType = "VARCHAR(2000)";
    }
}
