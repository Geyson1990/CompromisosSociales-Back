using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.StaticVariables.Dto
{
    public class StaticVariableCreateDto
    {
        public string Name { get; set; }
        public StaticVariableFamily Family { get; set; }
        public List<StaticVariableOptionCreateDto> Options { get; set; }
    }
}
