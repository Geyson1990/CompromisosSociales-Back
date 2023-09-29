using Abp.Application.Services.Dto;
using Contable.Application;

namespace Contable.Application.SocialConflictSensibles.Dto
{
    public class SocialConflictSensibleLocationDto : EntityDto
    {
        public SocialConflictSensibleTerritorialUnitDto TerritorialUnit { get; set; }
        public SocialConflictSensibleDepartmentDto Department { get; set; }
        public SocialConflictSensibleProvinceDto Province { get; set; }
        public SocialConflictSensibleDistrictDto District { get; set; }
        public SocialConflictSensibleRegionDto Region { get; set; }
        public string Ubication { get; set; }
        public bool Remove { get; set; }
    }
}
