using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictAlertLocations")]
    public class SocialConflictAlertLocation : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictAlertLocationConsts.TerritorialUnitIdType)]
        [ForeignKey("SocialConflictAlert")]
        public int SocialConflictAlertId { get; set; }
        public SocialConflictAlert SocialConflictAlert { get; set; }

        [Column(TypeName = SocialConflictAlertLocationConsts.TerritorialUnitIdType)]
        [ForeignKey("TerritorialUnit")]
        public int TerritorialUnitId { get; set; }
        public TerritorialUnit TerritorialUnit { get; set; }

        [Column(TypeName = SocialConflictAlertLocationConsts.DepartmentIdType)]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [Column(TypeName = SocialConflictAlertLocationConsts.ProvinceIdType)]
        [ForeignKey("Province")]
        public int ProvinceId { get; set; }
        public Province Province { get; set; }

        [Column(TypeName = SocialConflictAlertLocationConsts.DistrictIdType)]
        [ForeignKey("District")]
        public int DistrictId { get; set; }
        public District District { get; set; }

        [Column(TypeName = SocialConflictAlertLocationConsts.RegionIdType)]
        [ForeignKey("Region")]
        public int? RegionId { get; set; }
        public Region Region { get; set; }

        [Column(TypeName = SocialConflictAlertLocationConsts.UbicationType)]
        public string Ubication { get; set; }
    }
}
