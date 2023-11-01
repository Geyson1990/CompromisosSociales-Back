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
       
        Task<List<SectorMeetSessionAgreementCompromise>> Get(SocialConflictGetInputDto input);
        Task Delete(EntityDto input);
        Task<EntityDto<long>> Create(SectorMeetSessionAgreementCompromiseDto input);
    }
}
