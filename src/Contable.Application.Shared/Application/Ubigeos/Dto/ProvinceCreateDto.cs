using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Ubigeos.Dto
{
    public class ProvinceCreateDto
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string Code { get; set; }
    }
}
