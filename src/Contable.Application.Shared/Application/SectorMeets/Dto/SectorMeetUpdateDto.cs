using Abp.Application.Services.Dto;
using Contable.Application.SectorMeetSessions.Dto;
using System.Collections.Generic;

namespace Contable.Application.SectorMeets.Dto
{
    public class SectorMeetUpdateDto : EntityDto
    {
        public bool ReplaceCode { get; set; }
        public int ReplaceYear { get; set; }
        public int ReplaceCount { get; set; }
        public string MeetName { get; set; }
        public EntityDto TerritorialUnit { get; set; }
        public EntityDto SocialConflict { get; set; }
        public List<SectorMeetSessionAttachmentDto> UploadFiles { get; set; }
        public int? Modality { get; set; }
        public int? MeetType { get; set; }
        public int? RiskLevel { get; set; }
        public string Object { get; set; }
        public int? RolId { get; set; }
        public int? State { get; set; }

    }
}
