using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Ubigeos.Dto
{
    public class RegionGetAllDto : EntityDto
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string Ubigeo { get; set; }
        public string Code { get; set; }
        public RegionDistrictGetAllDto District { get; set; }
    }
}
