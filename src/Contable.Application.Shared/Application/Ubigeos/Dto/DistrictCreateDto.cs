using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Ubigeos.Dto
{
    public class DistrictCreateDto : EntityDto
    {
        public int ProvinceId { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string Code { get; set; }
        public string Ubigeo { get; set; }
    }
}
