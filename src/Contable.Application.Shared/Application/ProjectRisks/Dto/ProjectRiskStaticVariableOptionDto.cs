using Abp.Application.Services.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectRisks.Dto
{
    public class ProjectRiskStaticVariableOptionDto : EntityDto
    {
        public ProjectRiskDinamicVariableDto DinamicVariable { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public bool Enabled { get; set; }
        public int Index { get; set; }
        public StaticVariableType Type { get; set; }
        public StaticVariableSite Site { get; set; }
        public decimal Value { get; set; }
        public List<ProjectRiskStaticVariableOptionDetailDto> Details { get; set; }
    }
}
