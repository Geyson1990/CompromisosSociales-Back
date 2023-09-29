using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.HelpMemories.Dto
{
    public class HelpMemoryGetAllDto : EntityDto
    {
        public HelpMemorySocialConflictDto SocialConflict { get; set; }
        public HelpMemorySocialConflictSensibleDto SocialConflictSensible { get; set; }
        public HelpMemoryDirectoryGovernmentDto DirectoryGovernment { get; set; }

        public string Code { get; set; }
        public ConflictSite Site { get; set; }
        public string Request { get; set; }
        public DateTime RequestTime { get; set; }
    }
}
