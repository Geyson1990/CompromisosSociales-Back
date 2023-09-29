using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.Managements.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.Managements
{
    public interface IManagementAppService : IApplicationService
    {
        Task Create(ManagementCreateDto input);
        Task Delete(EntityDto input);
        Task<ManagementGetDto> Get(EntityDto input);
        Task<PagedResultDto<ManagementGetAllDto>> GetAll(ManagementGetAllInputDto input);
        Task Update(ManagementUpdateDto input);
    }
}
