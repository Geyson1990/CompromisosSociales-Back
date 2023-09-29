using Abp.Application.Services.Dto;
using Contable.Application;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictLocationDto : EntityDto
    {
        public SocialConflictTerritorialUnitDto TerritorialUnit { get; set; }
        public SocialConflictDepartmentDto Department { get; set; }
        public SocialConflictProvinceDto Province { get; set; }
        public SocialConflictDistrictDto District { get; set; }
        public SocialConflictRegionDto Region { get; set; }
        public string Ubication { get; set; }
        public bool Remove { get; set; }
    }
}
