using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSectorMeetSessionResources")]
    public class SectorMeetSessionResource : FullAuditedEntity
    {
        [Column(TypeName = SectorMeetSessionResourceConsts.SectorMeetSessionIdType)]
        [ForeignKey("SectorMeetSession")]
        public int SectorMeetSessionId { get; set; }
        public SectorMeetSession SectorMeetSession { get; set; }

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
