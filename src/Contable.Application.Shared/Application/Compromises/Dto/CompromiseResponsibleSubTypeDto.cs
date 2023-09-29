using Abp.Application.Services.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Compromises.Dto
{
    public class CompromiseResponsibleSubTypeDto : EntityDto
    {
        public string Name { get; set; }

        [JsonIgnore]
        public bool Enabled { get; set; }
    }
}
