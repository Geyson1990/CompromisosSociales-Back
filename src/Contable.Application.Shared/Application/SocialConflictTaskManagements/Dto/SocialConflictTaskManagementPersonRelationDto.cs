using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictTaskManagements.Dto
{
    public class SocialConflictTaskManagementPersonRelationDto : EntityDto
    {
        public SocialConflictTaskManagementPersonDto Person { get; set; }
        public bool Remove { get; set; }
    }
}
