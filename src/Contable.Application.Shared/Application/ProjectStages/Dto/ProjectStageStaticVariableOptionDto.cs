using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectStages.Dto
{
    public class ProjectStageStaticVariableOptionDto : EntityDto
    {
        public ProjectStageDinamicVariableDto DinamicVariable { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public bool Enabled { get; set; }
        public StaticVariableType Type { get; set; }
        public StaticVariableSite Site { get; set; }
        public decimal Value { get; set; }
    }
}
