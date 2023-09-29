using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.AlertResponsibles.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.AlertResponsibles
{
    public interface IAlertResponsibleAppService : IApplicationService
    {
        Task Create(AlertResponsibleCreateDto input);
        Task Delete(EntityDto input);
        Task<AlertResponsibleGetDto> Get(EntityDto input);
        Task<PagedResultDto<AlertResponsibleGetAllDto>> GetAll(AlertResponsibleGetAllInputDto input);
        Task Update(AlertResponsibleUpdateDto input);
    }
}
