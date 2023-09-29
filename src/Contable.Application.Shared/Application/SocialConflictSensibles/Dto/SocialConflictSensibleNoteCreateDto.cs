using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictSensibles.Dto
{
    public class SocialConflictSensibleNoteCreateDto
    {
        public int SocialConflictSensibleId { get; set; }
        public string Description { get; set; }
    }
}
