using Abp.Authorization;
using Abp.Localization;
using Abp.Notifications;
using Contable.Authorization;

namespace Contable.Notifications
{
    public class AppNotificationProvider : NotificationProvider
    {
        public override void SetNotifications(INotificationDefinitionContext context)
        {
            context.Manager.Add(new NotificationDefinition(
                name: AppNotificationNames.TaskManagementConflict,
                displayName: L("NewTaskManagementConflictNotification"),
                permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Application_SocialConflictTaskManagement)
            ));

            context.Manager.Add(new NotificationDefinition(
                name: AppNotificationNames.TaskManagementCompromise,
                displayName: L("NewTaskManagementCompromiseNotification"),
                permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Application_TaskManagement)
            ));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ContableConsts.LocalizationSourceName);
        }
    }
}
