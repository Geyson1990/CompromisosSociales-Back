using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contable.Application
{
    [Table("AppSectorMeetResources")]
    public class SectorMeetResource : FullAuditedEntity
    {
        [Column(TypeName = SectorMeetSessionResourceConsts.SectorMeetSessionIdType)]
        [ForeignKey("SectorMeets")]
        public int SectorMeetId { get; set; }
        public SectorMeet SectorMeet { get; set; }

        [Column(TypeName = SectorMeetSessionResourceConsts.AssetType)]
        public string CommonFolder { get; set; }

        [Column(TypeName = SectorMeetSessionResourceConsts.AssetType)]
        public string ResourceFolder { get; set; }

        [Column(TypeName = SectorMeetSessionResourceConsts.AssetType)]
        public string SectionFolder { get; set; }

        [Column(TypeName = SectorMeetSessionResourceConsts.AssetType)]
        public string FileName { get; set; }

        [Column(TypeName = SectorMeetSessionResourceConsts.AssetType)]
        public string Size { get; set; }

        [Column(TypeName = SectorMeetSessionResourceConsts.AssetType)]
        public string Extension { get; set; }

        [Column(TypeName = SectorMeetSessionResourceConsts.AssetType)]
        public string ClassName { get; set; }

        [Column(TypeName = SectorMeetSessionResourceConsts.AssetType)]
        public string Name { get; set; }

        [Column(TypeName = SectorMeetSessionResourceConsts.ResourceType)]
        public string Resource { get; set; }
    }
}
