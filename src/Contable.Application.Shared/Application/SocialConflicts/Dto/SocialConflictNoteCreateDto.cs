using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictNoteCreateDto 
    {
        public int SocialConflictId { get; set; }
        public string Description { get; set; }
    }
}
