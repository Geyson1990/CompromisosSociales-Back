using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.InterventionPlanOptions.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.InterventionPlanOptions
{
    public interface IInterventionPlanOptionAppService : IApplicationService
    {
        Task Create(InterventionPlanOptionCreateDto input);
        Task Delete(EntityDto input);
        Task<InterventionPlanOptionGetDto> Get(EntityDto input);
        Task<PagedResultDto<InterventionPlanOptionGetAllDto>> GetAll(InterventionPlanOptionGetAllInputDto input);
        Task Update(InterventionPlanOptionUpdateDto input);
    }
}
