using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.AlertDemands.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.AlertDemands
{
    public interface IAlertDemandAppService : IApplicationService
    {
        Task Create(AlertDemandCreateDto input);
        Task Delete(EntityDto input);
        Task<AlertDemandGetDto> Get(EntityDto input);
        Task<PagedResultDto<AlertDemandGetAllDto>> GetAll(AlertDemandGetAllInputDto input);
        Task Update(AlertDemandUpdateDto input);
    }
}
