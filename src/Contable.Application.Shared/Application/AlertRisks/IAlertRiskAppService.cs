using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.AlertRisks.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.AlertRisks
{
    public interface IAlertRiskAppService : IApplicationService
    {
        Task Create(AlertRiskCreateDto input);
        Task Delete(EntityDto input);
        Task<AlertRiskGetDto> Get(EntityDto input);
        Task<PagedResultDto<AlertRiskGetAllDto>> GetAll(AlertRiskGetAllInputDto input);
        Task Update(AlertRiskUpdateDto input);
    }
}
