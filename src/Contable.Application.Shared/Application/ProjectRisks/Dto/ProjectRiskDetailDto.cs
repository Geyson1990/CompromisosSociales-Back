using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectRisks.Dto
{
    public class ProjectRiskDetailDto : EntityDto
    {
        public int ProjectRiskId { get; set; }
        public int ProjectStageDetailId { get; set; }
        public int StaticVariableOptionId { get; set; }
        public bool Enabled { get; set; }
        public decimal Value { get; set; }
    }
}
