using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.Actors.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.Actors
{
    public interface IActorAppService : IApplicationService
    {
        Task<EntityDto<long>> Create(ActorCreateDto input);
        Task Delete(EntityDto input);
        Task<ActorGetDataDto> Get(NullableIdDto input);
        Task<PagedResultDto<ActorGetAllDto>> GetAll(ActorGetAllInputDto input);
        Task<EntityDto> Update(ActorUpdateDto input);
    }
}
