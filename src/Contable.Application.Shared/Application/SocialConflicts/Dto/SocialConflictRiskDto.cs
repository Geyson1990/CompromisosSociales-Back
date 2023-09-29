using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictRiskDto : EntityDto
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
