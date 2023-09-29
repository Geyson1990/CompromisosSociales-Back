using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.InterventionPlanActivities.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.InterventionPlanActivities
{
    public interface IInterventionPlanActivityAppService : IApplicationService
    {
        Task Create(InterventionPlanActivityCreateDto input);
        Task Delete(EntityDto input);
        Task<InterventionPlanActivityGetDto> Get(EntityDto input);
        Task<PagedResultDto<InterventionPlanActivityGetAllDto>> GetAll(InterventionPlanActivityGetAllInputDto input);
        Task Update(InterventionPlanActivityUpdateDto input);
    }
}
