using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictGeneralFactDto : EntityDto
    {
        public DateTime CreationTime { get; set; }
        public DateTime FactTime { get; set; }
        public string Description { get; set; }
        public bool Remove { get; set; }
    }
}
