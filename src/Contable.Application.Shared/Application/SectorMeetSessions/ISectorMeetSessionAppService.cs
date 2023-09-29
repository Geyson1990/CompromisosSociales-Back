using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.SectorMeetSessions.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.SectorMeetSessions
{
    public interface ISectorMeetSessionAppService : IApplicationService
    {
        Task<EntityDto> Create(SectorMeetSessionCreateDto input);
        Task Delete(EntityDto input);
        Task<SectorMeetSessionGetDataDto> Get(SectorMeetSessionGetInputDto input);
        Task<PagedResultDto<SectorMeetSessionGetAllDto>> GetAll(SectorMeetSessionGetAllInputDto input);
        Task<EntityDto> Update(SectorMeetSessionUpdateDto input);
    }
}
