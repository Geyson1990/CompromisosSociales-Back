using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectRisks.Dto
{
    public class ProjectRiskStageDto : EntityDto
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public List<ProjectRiskStageDetailDto> Details { get; set; }
    }
}
