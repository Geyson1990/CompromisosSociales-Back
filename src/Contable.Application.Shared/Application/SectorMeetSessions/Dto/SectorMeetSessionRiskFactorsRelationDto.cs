using Abp.Application.Services.Dto;

namespace Contable.Application.SectorMeetSessions.Dto
{
    public class SectorMeetSessionRiskFactorsRelationDto : EntityDto
    {
        public string Description { get; set; }
        public bool Remove { get; set; }
    }
}
