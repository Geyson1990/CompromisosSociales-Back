using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SectorMeetSessions.Dto
{
    public class SectorMeetSessionGetDataDto
    {
        public SectorMeetSessionGetDto SectorMeetSession { get; set; }
        public List<SectorMeetSessionDepartmentRelationDto> Departments { get; set; }
        public List<SectorMeetSessionPersonRelationDto> Persons { get; set; }
    } 
}
