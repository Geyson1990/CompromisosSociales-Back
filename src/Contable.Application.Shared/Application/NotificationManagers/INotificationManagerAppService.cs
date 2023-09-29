using Abp.Application.Services;
using Contable.Application.NotificationManagers.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.NotificationManagers
{
    public interface INotificationManagerAppService : IApplicationService
    {
        Task SendConflictTaskManagement(NotificationManagerConflictTaskDto input);
        Task SendCompromiseTaskManagement(NotificationManagerCompromiseDto input);
    }
}
