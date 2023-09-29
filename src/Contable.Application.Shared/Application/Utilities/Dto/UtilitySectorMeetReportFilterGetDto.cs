using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilitySectorMeetReportFilterGetDto
    {
        public List<UtilityDepartmentDataDto> Departments { get; set; }
        public List<UtilityPersonDto> Persons { get; set; }
    }
}
