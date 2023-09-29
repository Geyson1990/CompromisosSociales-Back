using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.CompromiseSubStates.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.CompromiseSubStates
{
    public interface ICompromiseSubStateAppService : IApplicationService
    {
        Task Create(CompromiseSubStateCreateDto input);
        Task Delete(EntityDto input);
        Task<CompromiseSubStateGetDto> Get(EntityDto input);
        Task<PagedResultDto<CompromiseSubStateGetAllDto>> GetAll(CompromiseSubStateGetAllInputDto input);
        Task Update(CompromiseSubStateUpdateDto input);
    }
}
