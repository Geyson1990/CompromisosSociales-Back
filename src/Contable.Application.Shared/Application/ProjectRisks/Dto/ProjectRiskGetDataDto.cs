using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectRisks.Dto
{
    public class ProjectRiskGetDataDto 
    {
        public ProjectRiskGetDto ProjectRisk { get; set; }
        public List<ProjectRiskStageDto> Stages { get; set; }
        public List<ProjectRiskDepartmentDto> Departments { get; set; }
        public List<ProjectRiskDinamicVariableDetailDto> DinamicValues { get; set; }
    }
}
