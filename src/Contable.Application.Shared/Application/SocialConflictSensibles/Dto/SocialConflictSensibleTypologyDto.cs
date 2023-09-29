using Abp.Application.Services.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictSensibles.Dto
{
    public class SocialConflictSensibleTypologyDto : EntityDto
    {
        public string Name { get; set; }
        [JsonIgnore]
        public bool Enabled { get; set; }
    }
}
