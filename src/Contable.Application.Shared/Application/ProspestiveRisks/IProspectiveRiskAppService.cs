using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.ProspestiveRisks.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.ProspestiveRisks
{
    public interface IProspectiveRiskAppService : IApplicationService
    {
        Task Process();
        Task DeleteHistory(EntityDto input);
        Task<ProspectiveRiskGetDto> Get(EntityDto input);
        Task<ProspectiveRiskHistoryGetDto> GetHistory(EntityDto input);
        Task<EntityDto> CreateOrUpdate(ProspectiveRiskCreateOrUpdateDto input);
        Task<PagedResultDto<ProspectiveRiskGetAllDto>> GetAll(ProspectiveRiskGetAllInputDto input);
        Task<PagedResultDto<ProspectiveRiskHistoryDto>> GetAllHistories(ProspectiveRiskHistoryGetAllInputDto input);
    }
}
