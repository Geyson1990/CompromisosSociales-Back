using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Ubigeos.Dto
{
    public class RegionProvinceGetAllDto : EntityDto
    {
        public string Name { get; set; }
        public RegionDepartmentGetAllDto Department { get; set; }
    }
}
