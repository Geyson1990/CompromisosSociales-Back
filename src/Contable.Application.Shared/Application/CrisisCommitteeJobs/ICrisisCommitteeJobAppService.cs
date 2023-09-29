using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.CrisisCommitteeJobs.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.CrisisCommitteeJobs
{
    public interface ICrisisCommitteeJobAppService : IApplicationService
    {
        Task Create(CrisisCommitteeJobCreateDto input);
        Task Delete(EntityDto input);
        Task<CrisisCommitteeJobGetDto> Get(EntityDto input);
        Task<PagedResultDto<CrisisCommitteeJobGetAllDto>> GetAll(CrisisCommitteeJobGetAllInputDto input);
        Task Update(CrisisCommitteeJobUpdateDto input);
    }
}
