using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contable.Application
{
    [Table("AppSocialConflictLocations")]
    public class SocialConflictLocation : FullAuditedEntity
    {
        public SocialConflict SocialConflict { get; set; }
        public TerritorialUnit TerritorialUnit { get; set; }
        public Department Department { get; set; }
        public Province Province { get; set; }
        public District District { get; set; }

        [Column(TypeName = SocialConflictLocationConsts.RegionIdType)]
        [ForeignKey("Region")]
        public int? RegionId { get; set; }
        public Region Region { get; set; }

        [Column(TypeName = SocialConflictLocationConsts.UbicationType)]
        public string Ubication { get; set; }

        public List<CompromiseLocation> CompromiseLocations { get; set; }
    }
}
