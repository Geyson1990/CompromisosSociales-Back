using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilitySocialConflictReportFilterGetDto
    {
        public List<UtilityDepartmentDataDto> Departments { get; set; }
        public List<UtilityTerritorialUnitDto> TerritorialUnits { get; set; }
        public List<UtilityTypologyDto> Typologies { get; set; }
        public List<UtilityPersonDto> Persons { get; set; }
    }
}
