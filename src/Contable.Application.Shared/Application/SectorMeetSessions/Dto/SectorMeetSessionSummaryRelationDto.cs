using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SectorMeetSessions.Dto
{
    public class SectorMeetSessionSummaryRelationDto : EntityDto
    {
        public string Description { get; set; }
        public SectorMeetSessionLeaderRelationDto SectorMeetSessionLeader { get; set; }
        public bool Remove { get; set; }
    }
}
