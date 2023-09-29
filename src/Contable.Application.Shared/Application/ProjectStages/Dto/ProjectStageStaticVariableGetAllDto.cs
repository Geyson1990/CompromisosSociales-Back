using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectStages.Dto
{
    public class ProjectStageStaticVariableGetAllDto : EntityDto
    {
        public string Name { get; set; }
        public StaticVariableFamily Family { get; set; }
        public bool Enabled { get; set; }
        public List<ProjectStageStaticVariableOptionDto> Options { get; set; }
    }
}
