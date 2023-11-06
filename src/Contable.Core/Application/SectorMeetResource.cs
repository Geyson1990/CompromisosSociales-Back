using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contable.Application
{
    [Table("AppSectorMeetResources")]
    public class SectorMeetResource : FullAuditedEntity
    {
        [Column(TypeName = SectorMeetResourceConsts.SectorMeetSessionIdType)]
        [ForeignKey("SectorMeet")]
        public int SectorMeetId { get; set; }
        public SectorMeet SectorMeet { get; set; }

        [Column(TypeName = SectorMeetResourceConsts.AssetType)]
        public string CommonFolder { get; set; }

        [Column(TypeName = SectorMeetResourceConsts.AssetType)]
        public string ResourceFolder { get; set; }

        [Column(TypeName = SectorMeetResourceConsts.AssetType)]
        public string SectionFolder { get; set; }

        [Column(TypeName = SectorMeetResourceConsts.AssetType)]
        public string FileName { get; set; }

        [Column(TypeName = SectorMeetResourceConsts.AssetType)]
        public string Size { get; set; }

        [Column(TypeName = SectorMeetResourceConsts.AssetType)]
        public string Extension { get; set; }

        [Column(TypeName = SectorMeetResourceConsts.AssetType)]
        public string ClassName { get; set; }

        [Column(TypeName = SectorMeetResourceConsts.AssetType)]
        public string Name { get; set; }

        [Column(TypeName = SectorMeetResourceConsts.ResourceType)]
        public string Resource { get; set; }
        [Column(TypeName = SectorMeetResourceConsts.ResourceType)]
        public string Description { get; set; }
    }
}
