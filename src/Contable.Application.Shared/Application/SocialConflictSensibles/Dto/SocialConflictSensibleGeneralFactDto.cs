using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictSensibles.Dto
{
    public class SocialConflictSensibleGeneralFactDto : EntityDto
    {
        public DateTime CreationTime { get; set; }
        public DateTime FactTime { get; set; }
        public string Description { get; set; }
        public bool Remove { get; set; }
    }
}
