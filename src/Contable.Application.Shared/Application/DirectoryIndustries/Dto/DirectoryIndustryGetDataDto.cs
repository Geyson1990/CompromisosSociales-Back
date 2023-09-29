using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DirectoryIndustries.Dto
{
    public class DirectoryIndustryGetDataDto
    {
        public DirectoryIndustryGetDto DirectoryIndustry { get; set; }
        public List<DirectoryIndustryDepartmentDto> Departments { get; set; }
        public List<DirectoryIndustrySectorDto> Sectors { get; set; }
    }
}
