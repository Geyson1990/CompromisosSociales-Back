using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.SocialConflictAlerts.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.SocialConflictAlerts
{
    public interface ISocialConflictAlertAppService : IApplicationService
    {
        Task<EntityDto> Create(SocialConflictAlertCreateDto input);
        Task<SocialConflictAlertGetDataDto> Get(NullableIdDto input);
        Task<PagedResultDto<SocialConflictAlertGetAllDto>> GetAll(SocialConflictAlertGetAllInputDto input);
        Task Update(SocialConflictAlertUpdateDto input);
        Task<SocialConflictAlertEmailConfigurationDto> GetEmailConfiguration(EntityDto input);
        Task SendAlert(SocialConflictAlertSendNotificationDto input);
    }
}
