using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Ubigeos.Dto
{
    public class RegionCreateDto
    {
        public int DistrictId { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string Code { get; set; }
    }
}
