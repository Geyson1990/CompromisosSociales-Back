using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.SectorMeets.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.SectorMeets
{
    public interface ISectorMeetAppService : IApplicationService
    {
        Task<EntityDto> Create(SectorMeetCreateDto input);
        Task Delete(EntityDto input);
        Task<SectorMeetGetDataDto> Get(NullableIdDto input);
        Task<PagedResultDto<SectorMeetGetAllDto>> GetAll(SectorMeetGetAllInputDto input);
        Task<EntityDto> Update(SectorMeetUpdateDto input);
    }
}
