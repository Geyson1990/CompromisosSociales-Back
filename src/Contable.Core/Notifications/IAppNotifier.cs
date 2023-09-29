using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Abp.Localization;
using Abp.Notifications;
using Contable.Application;
using Contable.Authorization.Users;
using Contable.MultiTenancy;

namespace Contable.Notifications
{
    public interface IAppNotifier
    {
        Task SendTaskManagementCompromiseNotificationAsync(List<User> users, TaskManagement task, string title);
        Task SendTaskManagementConflictNotificationAsync(List<User> users, SocialConflictTaskManagement task, string title);
    }
}
