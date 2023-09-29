using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SectorMeetSessions.Dto
{
    public class SectorMeetSessionCriticalAspectRelationDto : EntityDto
    {
        public string Description { get; set; }
        public bool Remove { get; set; }
    }
}
