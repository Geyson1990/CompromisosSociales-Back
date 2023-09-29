using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Portal.Dto
{
    public class PortalProvinceDto : EntityDto
    {
        public string Name { get; set; }
        public List<PortalDistrictDto> Districts { get; set; }
    }
}
