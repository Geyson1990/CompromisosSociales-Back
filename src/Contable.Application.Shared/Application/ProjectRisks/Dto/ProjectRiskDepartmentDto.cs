using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectRisks.Dto
{
    public class ProjectRiskDepartmentDto : EntityDto
    {
        public string Name { get; set; }
        public List<ProjectRiskProvinceDto> Provinces { get; set; }
        public List<ProjectRiskDepartmentTerrotorialUnitDto> TerritorialUnitDepartments { get; set; }
    }
}
