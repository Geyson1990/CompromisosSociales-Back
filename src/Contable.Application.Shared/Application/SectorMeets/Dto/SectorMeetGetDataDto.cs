using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SectorMeets.Dto
{
    public class SectorMeetGetDataDto 
    {
        public SectorMeetGetDto SectorMeet { get; set; }
        public List<SectorMeetTerritorialUnitRelationDto> TerritorialUnits { get; set; }
    }
}
