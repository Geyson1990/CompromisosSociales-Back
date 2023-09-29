using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectRisks.Dto
{
    public class ProjectRiskProvinceGetAllDto : EntityDto
    {
        public string Name { get; set; }
        public ProjectRiskDepartmentGetAllDto Department { get; set; }
    }
}
 