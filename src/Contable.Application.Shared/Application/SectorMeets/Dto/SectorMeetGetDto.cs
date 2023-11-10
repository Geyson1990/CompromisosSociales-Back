using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SectorMeets.Dto
{
    public class SectorMeetGetDto : EntityDto
    {
        public string Code { get; set; }
        public int Year { get; set; }
        public int Count { get; set; }
        public string MeetName { get; set; }
        public SectorMeetTerritorialUnitRelationDto TerritorialUnit { get; set; }
        public SectorMeetSocialConflictRelationDto SocialConflict { get; set; }

        public List<SectorMeetResourceRelationDto> Resources { get; set; }

        public SectorMeetGetDto()
        {
            Resources = new List<SectorMeetResourceRelationDto>();
        }
    }
}
