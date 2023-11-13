using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.Meets.Dto;
using Contable.Application.SectorMeets.Dto;
using System.Threading.Tasks;

namespace Contable.Application.Meets
{
    public interface IMeetAppService : IApplicationService
    {
        Task<EntityDto> Create(MeetCreateDto input);
        Task Delete(EntityDto input);
        Task<SectorMeetGetDataDto> Get(NullableIdDto input);
        Task<PagedResultDto<SectorMeetGetAllDto>> GetAll(SectorMeetGetAllInputDto input);
        Task<EntityDto> Update(SectorMeetUpdateDto input);
    }


}