using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class SocialConflictAlertStateConsts
    {
        public const string SocialConflictAlertIdType = "INT";
        public const string StateTimeType = "DATETIME";
        
        public const int DescriptionMinLength = 0;
        public const int DescriptionMaxLength = 2000;
        public const string DescriptionType = "VARCHAR(2000)";
    }
}
