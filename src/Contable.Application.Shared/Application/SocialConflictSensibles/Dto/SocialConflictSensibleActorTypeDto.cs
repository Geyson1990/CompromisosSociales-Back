using Abp.Application.Services.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictSensibles.Dto
{
    public class SocialConflictSensibleActorTypeDto : EntityDto
    {
        public string Name { get; set; }
        [JsonIgnore]
        public bool Enabled { get; set; }
        public bool ShowDetail { get; set; }
        public bool ShowMovement { get; set; }
    }
}
