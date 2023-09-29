using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Ubigeos.Dto
{
    public class RegionDistrictGetAllDto : EntityDto
    {
        public string Name { get; set; }
        public string Ubigeo { get; set; }
        public RegionProvinceGetAllDto Province { get; set; }
    }
}
