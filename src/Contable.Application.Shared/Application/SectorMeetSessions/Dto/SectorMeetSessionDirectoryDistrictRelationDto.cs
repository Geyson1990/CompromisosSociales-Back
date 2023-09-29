using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SectorMeetSessions.Dto
{
    public class SectorMeetSessionDirectoryDistrictRelationDto : EntityDto
    {
        public string Name { get; set; }
        public SectorMeetSessionDirectoryProvinceRelationDto Province { get; set; }
    }
}
