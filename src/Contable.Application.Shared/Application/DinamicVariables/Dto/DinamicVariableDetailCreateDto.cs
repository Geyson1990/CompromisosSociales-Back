using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DinamicVariables.Dto
{
    public class DinamicVariableDetailCreateDto
    {
        public DinamicVariableProvinceDto Province { get; set; }
        public decimal Value { get; set; }
    }
}
