using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SectorMeetSessions.Dto
{
    public class SectorMeetSessionLeaderRelationDto : EntityDto
    {
        public SectorMeetSessionEntityType Type { get; set; }
        public SectorMeetSessionDirectoryGovernmentRelationDto DirectoryGovernment { get; set; }
        public SectorMeetSessionDirectoryIndustryRelationDto DirectoryIndustry { get; set; }
        public string Entity { get; set; }
        public string Role { get; set; }
        public List<SectorMeetSessionTeamRelationDto> Teams { get; set; }
        public bool Remove { get; set; }
    }
}
