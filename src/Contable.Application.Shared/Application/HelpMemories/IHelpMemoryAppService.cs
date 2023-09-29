using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.HelpMemories.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.HelpMemories
{
    public interface IHelpMemoryAppService : IApplicationService
    {
        Task<EntityDto> Create(HelpMemoryCreateDto input);
        Task Delete(EntityDto input);
        Task<HelpMemoryGetDto> Get(HelpMemoryGetDto input);
        Task<PagedResultDto<HelpMemoryGetAllDto>> GetAll(HelpMemoryGetAllInputDto input);
        Task<EntityDto> Update(HelpMemoryUpdateDto input);
    }
}
