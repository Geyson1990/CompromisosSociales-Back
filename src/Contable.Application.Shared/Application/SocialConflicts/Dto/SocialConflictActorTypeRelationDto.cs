using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictActorTypeRelationDto : EntityDto
    {
        public string Name { get; set; }
        public bool ShowDetail { get; set; }
        public bool ShowMovement { get; set; }
    }
}
