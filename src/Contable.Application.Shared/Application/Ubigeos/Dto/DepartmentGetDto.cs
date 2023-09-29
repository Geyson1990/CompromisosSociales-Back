using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Ubigeos.Dto
{
    public class DepartmentGetDto : EntityDto
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string Code { get; set; }
    }
}
