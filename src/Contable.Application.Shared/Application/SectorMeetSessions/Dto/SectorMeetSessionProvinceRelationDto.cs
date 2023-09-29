using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SectorMeetSessions.Dto
{
    public class SectorMeetSessionProvinceRelationDto : EntityDto
    {
        public string Name { get; set; }
        public List<SectorMeetSessionDistrictRelationDto> Districts { get; set; }
    }
}
