using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectRisks.Dto
{
    public class ProjectRiskHistoryStaticVariableOptionGetDto : EntityDto
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public decimal Value { get; set; }
        public StaticVariableSite Site { get; set; }
        public List<ProjectRiskHistoryStaticVariableOptionDetailGetDto> Details { get; set; }
    }
}
