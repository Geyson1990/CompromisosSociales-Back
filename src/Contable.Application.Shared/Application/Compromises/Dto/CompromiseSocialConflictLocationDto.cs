using Abp.Application.Services.Dto;
using Contable.Application.SocialConflicts.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Compromises.Dto
{
    public class CompromiseSocialConflictLocationDto : EntityDto
    {
        public CompromiseTerritorialUnitDto TerritorialUnit { get; set; }
        public CompromiseDepartmentDto Department { get; set; }
        public CompromiseProvinceDto Province { get; set; }
        public CompromiseDistrictDto District { get; set; }
    }
}
