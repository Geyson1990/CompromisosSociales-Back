using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.SectorMeetSessions.Dto;
using Contable.Application.SectorMeetSessionsAgreement.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contable.Application.SectorMeetSessionsAgreement
{
    public interface ISectorMeetSessionAgreementAppService : IApplicationService
    {
       
        Task<List<SectorMeetSessionAgreementDto>> Get(SocialConflictGetInputDto input);
        Task<PagedResultDto<SectorMeetSessionGetAllDto>> GetAll(SectorMeetSessionGetAllInputDto input);
        Task<EntityDto> Update(SectorMeetSessionUpdateDto input);
    }
}
