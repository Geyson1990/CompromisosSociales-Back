using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DirectoryIndustries.Dto
{
    public class DirectoryIndustryProvinceReverseDto : EntityDto
    {
        public string Name { get; set; }
        public DirectoryIndustryDepartmentReverseDto Department { get; set; }
    }
}
