using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilityProvinceDataDto : EntityDto
    {
        public string Name { get; set; }
        public List<UtilityDistrictDataDto> Districts { get; set; }
    }
}
