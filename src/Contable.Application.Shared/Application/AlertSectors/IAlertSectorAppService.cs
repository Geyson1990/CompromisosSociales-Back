using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.AlertSectors.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.AlertSectors
{
    public interface IAlertSectorAppService : IApplicationService
    {
        Task Create(AlertSectorCreateDto input);
        Task Delete(EntityDto input);
        Task<AlertSectorGetDto> Get(EntityDto input);
        Task<PagedResultDto<AlertSectorGetAllDto>> GetAll(AlertSectorGetAllInputDto input);
        Task Update(AlertSectorUpdateDto input);
    }
}
