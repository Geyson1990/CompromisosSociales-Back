using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictDepartmentDto : EntityDto
    {
        public int[] TerritorialUnitIds { get; set; }
        public string Name { get; set; }
        public List<SocialConflictProvinceDto> Provinces { get; set; }
    }
}
