using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.AlertSeals.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.AlertSeals
{
    public interface IAlertSealAppService : IApplicationService
    {
        Task Create(AlertSealCreateDto input);
        Task Delete(EntityDto input);
        Task<AlertSealGetDto> Get(EntityDto input);
        Task<PagedResultDto<AlertSealGetAllDto>> GetAll(AlertSealGetAllInputDto input);
        Task Update(AlertSealUpdateDto input);
    }
}
