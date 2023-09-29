using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Notifications;
using Abp.UI;
using Contable.Application.Extensions;
using Contable.Application.NotificationManagers;
using Contable.Application.NotificationManagers.Dto;
using Contable.Authorization;
using Contable.Authorization.Users;
using Contable.Notifications;
using Microsoft.EntityFrameworkCore;
using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application
{
    [AbpAuthorize]
    public class NotificationManagerAppService : ContableAppServiceBase, INotificationManagerAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<TaskManagement, long> _taskManagementRepository;
        private readonly IRepository<SocialConflictTaskManagement> _socialConflictTaskManagementRepository;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly IAppNotifier _appNotifier;

        public NotificationManagerAppService(
            IRepository<User, long> userRepository, 
            IRepository<TaskManagement, long> taskManagementRepository, 
            IRepository<SocialConflictTaskManagement> socialConflictTaskManagementRepository,
            INotificationSubscriptionManager notificationSubscriptionManager,
            IAppNotifier appNotifier)
        {
            _userRepository = userRepository;
            _taskManagementRepository = taskManagementRepository;
            _socialConflictTaskManagementRepository = socialConflictTaskManagementRepository;
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _appNotifier = appNotifier;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task SendConflictTaskManagement(NotificationManagerConflictTaskDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Subject))
                throw new UserFriendlyException("Aviso", "El asunto de la notificación es obligatorio. Por favor verifique la información antes de continuar.");

            if (await _socialConflictTaskManagementRepository.CountAsync(p => p.Id == input.TaskId) == 0)
                throw new UserFriendlyException("Aviso", "La tarea seleccionada es inválida o ya no existe. Por favor verifique la información antes de continuar.");

            var task = _socialConflictTaskManagementRepository
                .GetAll()
                .Include(p => p.Persons)
                .ThenInclude(p => p.Person)
                .ThenInclude(p => p.User)
                .Where(p => p.Id == input.TaskId)
                .First();

            var users = task
                .Persons
                .Where(p => p.Person != null && p.Person.User != null)
                .Select(p => p.Person.User)
                .DistinctBy(p => p.Id)
                .ToList();

            await SubscribeAllUserToNotifications(users);
            await _appNotifier.SendTaskManagementConflictNotificationAsync(users, task, input.Subject);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task SendCompromiseTaskManagement(NotificationManagerCompromiseDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Subject))
                throw new UserFriendlyException("Aviso", "El asunto de la notificación es obligatorio. Por favor verifique la información antes de continuar.");

            if (await _taskManagementRepository.CountAsync(p => p.Id == input.TaskId) == 0)
                throw new UserFriendlyException("Aviso", "La tarea seleccionada es inválida o ya no existe. Por favor verifique la información antes de continuar.");

            var task = _taskManagementRepository
                .GetAll()
                .Include(p => p.Compromise)
                .Include(p => p.Persons)
                .ThenInclude(p => p.Person)
                .ThenInclude(p => p.User)
                .Where(p => p.Id == input.TaskId)
                .First();

            var users = task
                .Persons
                .Where(p => p.Person != null && p.Person.User != null)
                .Select(p => p.Person.User)
                .DistinctBy(p => p.Id)
                .ToList();

            await SubscribeAllUserToNotifications(users);
            await _appNotifier.SendTaskManagementCompromiseNotificationAsync(users, task, input.Subject);
        }

        private async Task SubscribeAllUserToNotifications(List<User> users)
        {
            foreach (var user in users)
                await _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync(user.ToUserIdentifier());

            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}
