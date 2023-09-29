using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.Risks.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.Risks
{
    public interface IRiskAppService : IApplicationService
    {
        Task Create(RiskCreateDto input);
        Task Delete(EntityDto input);
        Task<RiskGetDto> Get(EntityDto input);
        Task<PagedResultDto<RiskGetAllDto>> GetAll(RiskGetAllInputDto input);
        Task Update(RiskUpdateDto input);
    }
}
