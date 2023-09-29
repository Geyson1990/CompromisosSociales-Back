using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectRisks.Dto
{
    public class ProjectRiskDetailCreateOrUpdateDto : NullableIdDto
    {
        public int ProjectStageDetailId { get; set; }
        public int StaticVariableOptionId { get; set; }
        public decimal Value { get; set; }
    }
}
