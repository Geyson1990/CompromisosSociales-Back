using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictSensibles.Dto
{
    public class SocialConflictSensibleProvinceDto : EntityDto
    {
        public string Name { get; set; }
        public List<SocialConflictSensibleDistrictDto> Districts { get; set; }
    }
}
