using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.CompromiseStates.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.CompromiseStates
{
    public interface ICompromiseStateAppService : IApplicationService
    {
        Task Create(CompromiseStateCreateDto input);
        Task Delete(EntityDto input);
        Task<CompromiseStateGetDto> Get(EntityDto input);
        Task<PagedResultDto<CompromiseStateGetAllDto>> GetAll(CompromiseStateGetAllInputDto input);
        Task Update(CompromiseStateUpdateDto input);
    }
}
