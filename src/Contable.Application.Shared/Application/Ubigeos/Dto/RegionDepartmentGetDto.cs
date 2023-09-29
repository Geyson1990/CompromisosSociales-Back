using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Ubigeos.Dto
{
    public class RegionDepartmentGetDto : EntityDto
    {
        public string Name { get; set; }
        public List<RegionProvinceGetDto> Provinces { get; set; }
    }
}
