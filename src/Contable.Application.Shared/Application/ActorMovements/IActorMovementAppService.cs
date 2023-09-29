using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.ActorMovements.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.ActorMovements
{
    public interface IActorMovementAppService : IApplicationService
    {
        Task Create(ActorMovementCreateDto input);
        Task Delete(EntityDto input);
        Task<ActorMovementGetDto> Get(EntityDto input);
        Task<PagedResultDto<ActorMovementGetAllDto>> GetAll(ActorMovementGetAllInputDto input);
        Task Update(ActorMovementUpdateDto input);
    }
}
