using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.ProjectRisks.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.ProjectRisks
{
    public interface IProjectRiskAppService : IApplicationService
    {
        Task Delete(EntityDto input);
        Task DeleteHistory(EntityDto input);
        Task<ProjectRiskGetDataDto> Get(NullableIdDto input);
        Task<ProjectRiskHistoryGetDto> GetHistory(EntityDto input);
        Task<EntityDto> CreateOrUpdate(ProjectRiskCreateOrUpdateDto input);
        Task<PagedResultDto<ProjectRiskGetAllDto>> GetAll(ProjectRiskGetAllInputDto input);
        Task<ListResultDto<ProjectRiskDinamicVariableDetailDto>> GetAllDinamicValues(EntityDto input);
        Task<PagedResultDto<ProjectRiskHistoryGetAllDto>> GetAllHistories(ProjectRiskHistoryGetAllInputDto input);
    }
}
