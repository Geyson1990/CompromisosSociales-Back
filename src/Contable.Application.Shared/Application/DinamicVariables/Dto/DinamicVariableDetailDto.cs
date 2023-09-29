using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DinamicVariables.Dto
{
    public class DinamicVariableDetailDto
    {
        public List<DinamicVariableTerritorialUnitDto> TerritorialUnits { get; set; }
        public DinamicVariableDepartmentDto Department { get; set; }
        public DinamicVariableProvinceDto Province { get; set; }
        public int DinamicVariableId { get; set; }
        public int Id { get; set; }
        public decimal Value { get; set; }
    }
}
