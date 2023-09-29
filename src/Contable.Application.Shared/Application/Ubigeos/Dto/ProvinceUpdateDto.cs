using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Ubigeos.Dto
{
    public class ProvinceUpdateDto : EntityDto<long>
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string Code { get; set; }
    }
}
