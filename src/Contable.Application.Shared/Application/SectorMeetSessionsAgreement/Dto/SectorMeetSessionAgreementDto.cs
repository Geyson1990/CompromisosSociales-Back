using Abp.Application.Services.Dto;
using Contable.Application.SectorMeetSessions.Dto;
using Contable.Authorization.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SectorMeetSessionsAgreement.Dto
{
    public class SectorMeetSessionAgreementCompromise : EntityDto<long>
    {
        public string Description { get; set; }
        public int Index { get; set; }
        public int SectorMeetSessionId { get; set; }
        public long? CompromiseId { get; set; }
        public int? SectorMeetSessionAgreementId { get; set; }


        public int SocialConflictId { get; set; }
        public string Code { get; set; }
        public string CaseName { get; set; }
        public int SectorMeetId { get; set; }
        public string MeetName { get; set; }
        public int? PersonId { get; set; }
        public DateTime CreationTime { get; set; }
        public int AcuerdoAsignado { get; set; }

        public UserPersonDto Person { get; set; }
        public List<SectorMeetSessionResourceRelationDto> ResourceRelationDto { get; set; }
    }
}
