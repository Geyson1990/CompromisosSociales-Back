using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SectorMeetSessions.Dto
{
    public class SectorMeetSessionDepartmentRelationDto : EntityDto
    {
        public string Name { get; set; }
        public List<SectorMeetSessionProvinceRelationDto> Provinces { get; set; }
    }
}
