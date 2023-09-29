using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DinamicVariables.Dto
{
    public class DinamicVariableEntityUpdateDto
    {
        public DinamicVariableUpdateDto Variable { get; set; }
        public List<DinamicVariableDetailUpdateDto> Changes { get; set; }
    }
}
