using Abp.Application.Services.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectRisks.Dto
{
    public class ProjectRiskStaticVariableDto : EntityDto
    {
        public string Name { get; set; }
        [JsonIgnore]
        public bool Enabled { get; set; }
        public List<ProjectRiskStaticVariableOptionDto> Options { get; set; }
    }
}
