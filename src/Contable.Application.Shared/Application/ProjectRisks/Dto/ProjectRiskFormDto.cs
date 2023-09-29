using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectRisks.Dto
{
    public class ProjectRiskFormDto
    {
        public int StageDetailId { get; set; }
        public int StaticVariableId { get; set; }
        public int StaticVariableOptionId { get; set; }
        public decimal Weight { get; set; }
        public decimal Value { get; set; }
    }
}
