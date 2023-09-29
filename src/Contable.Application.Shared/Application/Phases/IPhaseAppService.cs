using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.Phases.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.Phases
{
    public interface IPhaseAppService : IApplicationService
    {
        Task Create(PhaseCreateDto input);
        Task Delete(EntityDto input);
        Task<PhaseGetDto> Get(EntityDto input);
        Task<PagedResultDto<PhaseGetAllDto>> GetAll(PhaseGetAllInputDto input);
        Task Update(PhaseUpdateDto input);
    }
}
