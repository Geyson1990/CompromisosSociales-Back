using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProspestiveRisks.Dto
{
    public class ProspectiveRiskFormDto
    {
        public int StaticVariableId { get; set; }
        public int StaticVariableOptionId { get; set; }
        public decimal Weight { get; set; }
        public decimal Value { get; set; }
    }
}
