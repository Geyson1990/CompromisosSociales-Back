using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilityGetDataDto
    {
        public List<UtilityTerritorialUnitDto> TerritorialUnits { get; set; }
        public List<UtilityDepartmentDataDto> Departments { get; set; }
        public List<UtilitySocialConflictDataDto> SocialConflicts { get; set; }
    }
}
