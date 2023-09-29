using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SectorMeetSessions.Dto
{
    public class SectorMeetSessionGetInputDto 
    {
        public int? SectorMeetSessionId { get; set; }
        public int SectorMeetId { get; set; }
    }
}
