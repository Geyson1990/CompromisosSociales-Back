using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SectorMeetSessions.Dto
{
    public class SectorMeetSessionCreateDto
    {
        public EntityDto SectorMeet { get; set; }
        public DateTime SessionTime { get; set; }
        public SectorMeetSessionType Type { get; set; }
        public EntityDto Department { get; set; }
        public EntityDto Province { get; set; }
        public EntityDto District { get; set; }
        public string Location { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MainSummary { get; set; }
        public bool IsDescriptionSocialConflict { get; set; }
        public string Side { get; set; }
        public DateTime? PersonTime { get; set; }
        public SectorMeetSessionPersonRelationDto Person { get; set; }

        public List<SectorMeetSessionActionRelationDto> Actions { get; set; }
        public List<SectorMeetSessionAgreementRelationDto> Agreements { get; set; }
        public List<SectorMeetSessionCriticalAspectRelationDto> CriticalAspects { get; set; }
        public List<SectorMeetSessionScheduleRelationDto> Schedules { get; set; }
        public List<SectorMeetSessionSummaryRelationDto> Summaries { get; set; }
        public List<SectorMeetSessionResourceRelationDto> Resources { get; set; }
        public List<SectorMeetSessionLeaderRelationDto> Leaders { get; set; }
        public List<SectorMeetSessionAttachmentDto> UploadFiles { get; set; }
        public List<SectorMeetSessionAttachmentDto> UploadFilesPDF { get; set; }
    }
}
