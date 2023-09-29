using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DialogSpaces.Dto
{
    public class DialogSpaceDepartmentDto : EntityDto
    {
        public int[] TerritorialUnitIds { get; set; }
        public string Name { get; set; }
        public List<DialogSpaceProvinceDto> Provinces { get; set; }
    }
}
