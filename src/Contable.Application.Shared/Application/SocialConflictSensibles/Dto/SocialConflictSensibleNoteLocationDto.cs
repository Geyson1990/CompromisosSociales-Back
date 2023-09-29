using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictSensibles.Dto
{
    public class SocialConflictSensibleNoteLocationDto : EntityDto
    {
        public string Description { get; set; }
        public bool Remove { get; set; }
    }
}
