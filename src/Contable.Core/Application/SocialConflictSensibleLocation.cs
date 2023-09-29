using Abp.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contable.Application
{
    [Table("AppSocialConflictSensibleLocations")]
    public class SocialConflictSensibleLocation : Entity
    {
        [Column(TypeName = SocialConflictSensibleLocationConsts.SocialConflictSensibleIdType)]
        [ForeignKey("SocialConflictSensible")]
        public int SocialConflictSensibleId { get; set; }
        public SocialConflictSensible SocialConflictSensible { get; set; }

        [Column(TypeName = SocialConflictSensibleLocationConsts.TerritorialUnitIdType)]
        [ForeignKey("TerritorialUnit")]
        public int TerritorialUnitId { get; set; }
        public TerritorialUnit TerritorialUnit { get; set; }

        [Column(TypeName = SocialConflictSensibleLocationConsts.DepartmentIdType)]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [Column(TypeName = SocialConflictSensibleLocationConsts.ProvinceIdType)]
        [ForeignKey("Province")]
        public int ProvinceId { get; set; }
        public Province Province { get; set; }

        [Column(TypeName = SocialConflictSensibleLocationConsts.DistrictIdType)]
        [ForeignKey("District")]
        public int DistrictId { get; set; }
        public District District { get; set; }

        [Column(TypeName = SocialConflictSensibleLocationConsts.RegionIdType)]
        [ForeignKey("Region")]
        public int? RegionId { get; set; }
        public Region Region { get; set; }

        [Column(TypeName = SocialConflictSensibleLocationConsts.UbicationType)]
        public string Ubication { get; set; }
    }
}
