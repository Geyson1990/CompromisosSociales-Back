using Contable.Application.Uploaders.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictSensibles.Dto
{
    public class SocialConflictSensibleCreateResourceDto
    {
        public int SocialConflictId { get; set; }
        public UploadResourceInputDto Resource { get; set; }
    }
}
