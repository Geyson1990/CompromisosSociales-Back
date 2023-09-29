using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.Managers.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.Managers
{
    public interface IManagerAppService : IApplicationService
    {
        Task Create(ManagerCreateDto input);
        Task Delete(EntityDto input);
        Task<ManagerGetDataDto> Get(NullableIdDto input);
        Task<PagedResultDto<ManagerGetAllDto>> GetAll(ManagerGetAllInputDto input);
        Task Update(ManagerUpdateDto input);
    }
}
