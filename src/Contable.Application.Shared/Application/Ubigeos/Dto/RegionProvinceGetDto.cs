using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Ubigeos.Dto
{
    public class RegionProvinceGetDto : EntityDto
    {
        public string Name { get; set; }
        public List<RegionDistrictGetDto> Districts { get; set; }
    }
}
