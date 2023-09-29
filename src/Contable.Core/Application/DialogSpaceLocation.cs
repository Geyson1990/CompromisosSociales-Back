using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDialogSpaceLocations")]
    public class DialogSpaceLocation : FullAuditedEntity
    {
        [Column(TypeName = DialogSpaceLocationConsts.DialogSpaceIdType)]
        [ForeignKey("DialogSpace")]
        public int DialogSpaceId { get; set; }
        public DialogSpace DialogSpace { get; set; }

        [Column(TypeName = DialogSpaceLocationConsts.TerritorialUnitIdType)]
        [ForeignKey("TerritorialUnit")]
        public int TerritorialUnitId { get; set; }
        public TerritorialUnit TerritorialUnit { get; set; }

        [Column(TypeName = DialogSpaceLocationConsts.DepartmentIdType)]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [Column(TypeName = DialogSpaceLocationConsts.ProvinceIdType)]
        [ForeignKey("Province")]
        public int ProvinceId { get; set; }
        public Province Province { get; set; }

        [Column(TypeName = DialogSpaceLocationConsts.DistrictIdType)]
        [ForeignKey("District")]
        public int DistrictId { get; set; }
        public District District { get; set; }

        [Column(TypeName = DialogSpaceLocationConsts.RegionIdType)]
        [ForeignKey("Region")]
        public int? RegionId { get; set; }
        public Region Region { get; set; }

        [Column(TypeName = DialogSpaceLocationConsts.UbicationType)]
        public string Ubication { get; set; }
    }
}
