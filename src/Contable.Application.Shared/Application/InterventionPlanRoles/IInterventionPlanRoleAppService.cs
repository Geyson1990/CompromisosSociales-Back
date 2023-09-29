using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.InterventionPlanRoles.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.InterventionPlanRoles
{
    public interface IInterventionPlanRoleAppService : IApplicationService
    {
        Task Create(InterventionPlanRoleCreateDto input);
        Task Delete(EntityDto input);
        Task<InterventionPlanRoleGetDto> Get(EntityDto input);
        Task<PagedResultDto<InterventionPlanRoleGetAllDto>> GetAll(InterventionPlanRoleGetAllInputDto input);
        Task Update(InterventionPlanRoleUpdateDto input);
    }
}
