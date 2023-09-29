using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictTaskManagements.Dto
{
    public class SocialConflictTaskManagementPersonChangeInputDto : EntityDto
    {
        public List<SocialConflictTaskManagementPersonChangeDto> Changes { get; set; }
    }
}
