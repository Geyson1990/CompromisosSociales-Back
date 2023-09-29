using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.InterventionPlanEntities.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.InterventionPlanEntities
{
    public interface IInterventionPlanEntityAppService : IApplicationService
    {
        Task Create(InterventionPlanEntityCreateDto input);
        Task Delete(EntityDto input);
        Task<InterventionPlanEntityGetDto> Get(EntityDto input);
        Task<PagedResultDto<InterventionPlanEntityGetAllDto>> GetAll(InterventionPlanEntityGetAllInputDto input);
        Task Update(InterventionPlanEntityUpdateDto input);
    }
}
