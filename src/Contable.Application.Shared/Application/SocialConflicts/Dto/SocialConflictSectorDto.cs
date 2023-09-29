using Abp.Application.Services.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictSectorDto : EntityDto
    {
        public string Name { get; set; }
        [JsonIgnore]
        public bool Enabled { get; set; }
    }
}
