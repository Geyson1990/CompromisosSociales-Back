using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SectorMeets.Dto
{
    public class SectorMeetCreateDto
    {
        public bool ReplaceCode { get; set; }
        public int ReplaceYear { get; set; }
        public int ReplaceCount { get; set; }
        public string MeetName { get; set; }
        public EntityDto TerritorialUnit { get; set; }
        public EntityDto SocialConflict { get; set; }
    }
}
