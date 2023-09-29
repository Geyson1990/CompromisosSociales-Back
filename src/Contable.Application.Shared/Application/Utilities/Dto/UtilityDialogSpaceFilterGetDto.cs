using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilityDialogSpaceFilterGetDto
    {
        public List<UtilityDepartmentDataDto> Departments { get; set; }
        public List<UtilityDialogSpaceTypeDto> DialogSpaceTypes { get; set; }
        public List<UtilityTerritorialUnitDto> TerritorialUnits { get; set; }
    }
}
