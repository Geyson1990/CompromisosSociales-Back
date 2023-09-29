using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DialogSpaces.Dto
{
    public class DialogSpaceProvinceDto : EntityDto
    {
        public string Name { get; set; }
        public List<DialogSpaceDistrictDto> Districts { get; set; }
    }
}
