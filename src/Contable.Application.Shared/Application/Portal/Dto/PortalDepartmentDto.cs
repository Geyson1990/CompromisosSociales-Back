using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Portal.Dto
{
    public class PortalDepartmentDto : EntityDto
    {
        public string Name { get; set; }
        public List<PortalProvinceDto> Provinces { get; set; }
        public List<EntityDto> TerritorialUnitIds { get; set; }
    }
}
