using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.CrisisCommittees.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.CrisisCommittees
{
    public interface ICrisisCommitteeAppService : IApplicationService
    {
        Task<EntityDto> Create(CrisisCommitteeCreateDto input);
        Task Delete(EntityDto input);
        Task<CrisisCommitteeGetDataDto> Get(NullableIdDto input);
        Task<PagedResultDto<CrisisCommitteeGetAllDto>> GetAll(CrisisCommitteeGetAllInputDto input);
        Task Update(CrisisCommitteeUpdateDto input);
    }
}
