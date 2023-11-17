using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SectorMeetSessions.Dto
{
    public class SectorMeetSessionGetAllDto : EntityDto
    {
        public DateTime? SessionTime { get; set; }
        public SectorMeetSessionType Type { get; set; }
        public SectorMeetSessionDepartmentReverseDto Department { get; set; }
        public SectorMeetSessionProvinceReverseDto Province { get; set; }
        public SectorMeetSessionDistrictReverseDto District { get; set; }
        public List<SectorMeetSessionRiskFactorsRelationDto> RiskFactors { get; set; }
        public string Location { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MainSummary { get; set; }
        public bool IsDescriptionSocialConflict { get; set; }
    }
}
