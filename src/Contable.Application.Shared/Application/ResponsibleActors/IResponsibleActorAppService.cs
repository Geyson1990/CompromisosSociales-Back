using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.ResponsibleActors.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.ResponsibleActors
{
    public interface ICompromiseStateAppService : IApplicationService
    {
        Task CreateResponsibleActor(ResponsibleActorCreateDto input);
        Task DeleteResponsibleAction(EntityDto input);
        Task<ResponsibleActorGetDataDto> GetResponsibleActor(NullableIdDto input);
        Task<PagedResultDto<ResponsibleActorGetAllDto>> GetAllResponsibleActors(ResponsibleActorGetAllInputDto input);
        Task UpdateResponsibleActor(ResponsibleActorUpdateDto input);

        Task CreateResponsibleSubActor(ResponsibleSubActorCreateDto input);
        Task DeleteResponsibleSubActor(EntityDto input);
        Task<ResponsibleSubActorGetDto> GetResponsibleSubActor(EntityDto input);
        Task UpdateResponsibleSubActor(ResponsibleSubActorUpdateDto input);
    }
}
