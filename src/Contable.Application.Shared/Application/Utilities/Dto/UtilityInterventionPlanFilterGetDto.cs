using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilityInterventionPlanFilterGetDto
    {
        public List<UtilityTerritorialUnitDto> TerritorialUnits { get; set; }
        public List<UtilityDepartmentDataDto> Departments { get; set; }
        public List<UtilityPersonDto> Persons { get; set; }
    }
}
