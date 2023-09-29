using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictSensibles.Dto
{
    public class SocialConflictSensibleDepartmentDto : EntityDto
    {
        public int[] TerritorialUnitIds { get; set; }
        public string Name { get; set; }
        public List<SocialConflictSensibleProvinceDto> Provinces { get; set; }
    }
}
