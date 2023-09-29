using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.ActorTypes.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.ActorTypes
{
    public interface IActorTypeAppService : IApplicationService
    {
        Task Create(ActorTypeCreateDto input);
        Task Delete(EntityDto input);
        Task<ActorTypeGetDto> Get(EntityDto input);
        Task<PagedResultDto<ActorTypeGetAllDto>> GetAll(ActorTypeGetAllInputDto input);
        Task Update(ActorTypeUpdateDto input);
    }
}
