using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Ubigeos.Dto
{
    public class RegionGetDataDto
    {
        public RegionGetDto Region { get; set; }
        public List<RegionDepartmentGetDto> Departments { get; set; }
    }
}
