using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Localization;
using Abp.Notifications;
using Contable.Application;
using Contable.Authorization.Users;
using Contable.MultiTenancy;

namespace Contable.Notifications
{
    public class AppNotifier : ContableDomainServiceBase, IAppNotifier
    {
        private readonly INotificationPublisher _notificationPublisher;

        public AppNotifier(INotificationPublisher notificationPublisher)
        {
            _notificationPublisher = notificationPublisher;
        }

        public async Task SendTaskManagementCompromiseNotificationAsync(List<User> users, TaskManagement task, string title)
        {
            var notificationData = new LocalizableMessageNotificationData(new LocalizableString(
                name: "NewTaskManagementCompromiseNotification",
                sourceName: ContableConsts.LocalizationSourceName
            ));

            notificationData["title"] = title;
            notificationData["id"] = task.Id;
            notificationData["description"] = task.Title;
            notificationData["compromiseId"] = task.Compromise.Id;

            await _notificationPublisher.PublishAsync(
                notificationName: AppNotificationNames.TaskManagementConflict,
                data: notificationData,
                userIds: users.Select(p => new UserIdentifier(ContableConsts.DefaultTenantId, p.Id)).ToArray(),
                severity: NotificationSeverity.Warn
            );
        }

        public async Task SendTaskManagementConflictNotificationAsync(List<User> users, SocialConflictTaskManagement task, string title)
        {
            var notificationData = new LocalizableMessageNotificationData(new LocalizableString(
                name: "NewTaskManagementConflictNotification",
                sourceName: ContableConsts.LocalizationSourceName
            ));

            notificationData["title"] = title;
            notificationData["id"] = task.Id;
            notificationData["description"] = task.Title;
            notificationData["type"] = task.Site;

            if(task.Site == ConflictSite.SocialConflict)
                notificationData["conflictId"] = task.SocialConflictId.Value;
            if (task.Site == ConflictSite.SocialConflictAlert)
                notificationData["conflictId"] = task.SocialConflictAlertId.Value;
            if (task.Site == ConflictSite.SocialConflictSensible)
                notificationData["conflictId"] = task.SocialConflictSensibleId.Value;

            await _notificationPublisher.PublishAsync(
                notificationName: AppNotificationNames.TaskManagementConflict,
                data: notificationData, 
                userIds: users.Select(p => new UserIdentifier(ContableConsts.DefaultTenantId, p.Id)).ToArray(),
                severity: NotificationSeverity.Warn
            );
        }
    }
}
