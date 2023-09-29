using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectStages.Dto
{
    public class ProjectStageDetailGetDto : EntityDto
    {
        public ProjectStageStaticVariableGetDto StaticVariable { get; set; }
    }
}
