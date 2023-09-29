using Contable.Application.Uploaders.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.HelpMemories.Dto
{
    public class HelpMemoryCreateDto
    {
        public HelpMemorySocialConflictDto SocialConflict { get; set; }
        public HelpMemorySocialConflictSensibleDto SocialConflictSensible { get; set; }
        public HelpMemoryDirectoryGovernmentDto DirectoryGovernment { get; set; }

        public string Request { get; set; }
        public DateTime RequestTime { get; set; }

        public List<HelpMemoryResourceRelationDto> Resources { get; set; }
        public List<UploadResourceInputDto> UploadFiles { get; set; }
    }
}
