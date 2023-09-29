using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SectorMeetSessions.Dto
{
    public class SectorMeetSessionDirectoryProvinceRelationDto : EntityDto
    {
        public string Name { get; set; }
        public SectorMeetSessionDirectoryDepartmentRelationDto Department { get; set; }
    }
}
