using Abp.Application.Services.Dto;
using System;

namespace Contable.Application.SectorMeets.Dto
{
    public class SectorMeetResourceRelationDto : EntityDto
    {
        public DateTime CreationTime { get; set; }
        public string CreatorUserName { get; set; }
        public string SectionFolder { get; set; }
        public string FileName { get; set; }
        public string Size { get; set; }
        public string Extension { get; set; }
        public string ClassName { get; set; }
        public string Name { get; set; }
        public string Resource { get; set; }
        public bool Remove { get; set; }
        public string Description { get; set; }
    }
}
