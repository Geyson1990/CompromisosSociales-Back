using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectRisks.Dto
{
    public class ProjectRiskDepartmentGetAllDto : EntityDto
    {
        public string Name { get; set; }

        public List<ProjectRiskDepartmentTerritorialUnitGetAllDto> TerritorialUnitDepartments { get; set; }
    }
}
