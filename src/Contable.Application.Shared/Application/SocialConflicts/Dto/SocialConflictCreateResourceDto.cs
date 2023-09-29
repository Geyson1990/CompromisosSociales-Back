using Contable.Application.Uploaders.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictCreateResourceDto
    {
        public int SocialConflictId { get; set; }
        public UploadResourceInputDto Resource { get; set; }
    }
}
