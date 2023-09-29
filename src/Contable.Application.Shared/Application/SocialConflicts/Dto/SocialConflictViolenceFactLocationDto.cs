using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictViolenceFactLocationDto : EntityDto
    {
        public SocialConflictDepartmentRelationDto Department { get; set; }
        public SocialConflictProvinceRelationDto Province { get; set; }
        public SocialConflictDistrictRelationDto District { get; set; }
        public SocialConflictRegionDto Region { get; set; }
        public string Ubication { get; set; }
        public bool Remove { get; set; }
    }
}
