using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.Analysts.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.Analysts
{
    public interface IAnalystAppService : IApplicationService
    {
        Task Create(AnalystCreateDto input);
        Task Delete(EntityDto input);
        Task<AnalystGetDto> Get(EntityDto input);
        Task<PagedResultDto<AnalystGetAllDto>> GetAll(AnalystGetAllInputDto input);
        Task Update(AnalystUpdateDto input);
    }
}
