using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictViolenceFactLocations")]
    public class SocialConflictViolenceFactLocation : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictViolenceFactLocationConsts.SocialConflictViolenceFactIdType)]
        public int SocialConflictViolenceFactId { get; set; }
        public SocialConflictViolenceFact SocialConflictViolenceFact { get; set; }

        [Column(TypeName = SocialConflictViolenceFactLocationConsts.DepartmentIdType)]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [Column(TypeName = SocialConflictViolenceFactLocationConsts.ProvinceIdType)]
        public int ProvinceId { get; set; }
        public Province Province { get; set; }

        [Column(TypeName = SocialConflictViolenceFactLocationConsts.DistrictIdType)]
        public int DistrictId { get; set; }
        public District District { get; set; }

        [Column(TypeName = SocialConflictLocationConsts.RegionIdType)]
        [ForeignKey("Region")]
        public int? RegionId { get; set; }
        public Region Region { get; set; }

        [Column(TypeName = SocialConflictViolenceFactLocationConsts.UbicationType)]
        public string Ubication { get; set; }
    }
}
