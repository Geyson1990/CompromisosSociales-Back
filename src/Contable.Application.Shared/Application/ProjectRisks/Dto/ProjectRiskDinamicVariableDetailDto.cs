using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectRisks.Dto
{
    public class ProjectRiskDinamicVariableDetailDto : EntityDto
    {
        public int DinamicVariableId { get; set; }
        public decimal Value { get; set; }
    }
}
