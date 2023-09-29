using Contable.Application.Uploaders.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictTaskManagements.Dto
{
    public class SocialConflictTaskManagementCreateResourceDto
    {
        public int TaskManagementId { get; set; }
        public UploadResourceInputDto Resource { get; set; }
    }
}
