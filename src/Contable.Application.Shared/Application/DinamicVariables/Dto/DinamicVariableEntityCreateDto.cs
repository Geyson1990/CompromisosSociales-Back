using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DinamicVariables.Dto
{
    public class DinamicVariableEntityCreateDto
    {
        public DinamicVariableCreateDto Variable { get; set; }
        public List<DinamicVariableDetailCreateDto> Changes { get; set; }
    }
}
