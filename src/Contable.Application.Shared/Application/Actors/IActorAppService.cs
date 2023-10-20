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
        Task Create(ActorCreateDto input);
        Task Delete(EntityDto input);
        Task<ActorGetDto> Get(EntityDto input);
        Task<PagedResultDto<ActorGetAllDto>> GetAll(ActorGetAllInputDto input);
        Task Update(ActorUpdateDto input);
    }
}
