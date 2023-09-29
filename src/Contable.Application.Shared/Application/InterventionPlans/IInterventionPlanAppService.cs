using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.InterventionPlans.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.InterventionPlans
{
    public interface IInterventionPlanAppService : IApplicationService
    {
        Task<EntityDto> Create(InterventionPlanCreateDto input);
        Task Delete(EntityDto input);
        Task<InterventionPlanGetDataDto> Get(NullableIdDto input);
        Task<PagedResultDto<InterventionPlanGetAllDto>> GetAll(InterventionPlanGetAllInputDto input);
        Task<PagedResultDto<InterventionPlanActorRelationDto>> GetAllActorByConflict(InterventionPlanActorGetAllInputDto input);
        Task<PagedResultDto<InterventionPlanLocationReferenceDto>> GetAllLocationByConflict(InterventionPlanLocationGetAllInputDto input);
        Task Update(InterventionPlanUpdateDto input);
    }
}
