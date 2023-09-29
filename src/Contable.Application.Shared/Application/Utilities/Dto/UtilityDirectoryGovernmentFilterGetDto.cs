using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilityDirectoryGovernmentFilterGetDto
    {
        public List<UtilityDepartmentDataDto> Departments { get; set; }
        public List<UtilityDirectoryGovernmentSectorDto> Sectors { get; set; }
    }
}
