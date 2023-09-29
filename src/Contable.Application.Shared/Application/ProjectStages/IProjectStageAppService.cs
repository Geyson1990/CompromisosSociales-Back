using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.ProjectStages.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.ProjectStages
{
    public interface IProjectStageAppService : IApplicationService
    {
        Task Create(ProjectStageCreateDto input);
        Task Delete(EntityDto input);
        Task Disable(EntityDto input);
        Task Enable(EntityDto input);
        Task<ProjectStageGetDto> Get(EntityDto input);
        Task<PagedResultDto<ProjectStageGetAllDto>> GetAll(ProjectStageGetAllInputDto input);
        Task<PagedResultDto<ProjectStageStaticVariableGetAllDto>> GetAllStaticVariables(ProjectStageStaticVariableGetAllInputDto input);
        Task Update(ProjectStageUpdateDto input);
    }
}
