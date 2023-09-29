using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictNoteUpdateDto : EntityDto
    {
        public string Description { get; set; }
    }
}
