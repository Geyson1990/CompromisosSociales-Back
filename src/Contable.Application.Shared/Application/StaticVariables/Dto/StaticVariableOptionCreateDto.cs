using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.StaticVariables.Dto
{
    public class StaticVariableOptionCreateDto
    {
        public StaticVariableCuantitativeDto DinamicVariable { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public bool Enabled { get; set; }
        public decimal Value { get; set; }
        public StaticVariableType Type { get; set; }
        public StaticVariableSite Site { get; set; }
        public List<StaticVariableOptionDetailCreateDto> Details { get; set; }
    }
}
