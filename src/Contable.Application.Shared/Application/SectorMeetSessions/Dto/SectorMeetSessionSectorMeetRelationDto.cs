using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SectorMeetSessions.Dto
{
    public class SectorMeetSessionSectorMeetRelationDto : EntityDto
    {
        public string Code { get; set; }
        public string MeetName { get; set; }
    }
}
