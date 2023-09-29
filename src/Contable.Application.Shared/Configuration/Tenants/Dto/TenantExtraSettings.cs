using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Configuration.Tenants.Dto
{
    public class TenantExtraSettings
    {
        public string SocialConflictAlertTemplate { get; set; }

        public string TaskManagementAlertSubject { get; set; }
        public string TaskManagementAlertExpirationSubject { get; set; }
        public string TaskManagementAlertNotificationSubject { get; set; }
        public string TaskManagementAlertTemplate { get; set; }

        public string SocialConflictTaskManagementAlertSubject { get; set; }
        public string SocialConflictTaskManagementAlertExpirationSubject { get; set; }
        public string SocialConflictTaskManagementAlertNotificationSubject { get; set; }
        public string SocialConflictTaskManagementAlertTemplate { get; set; }
    }
}
