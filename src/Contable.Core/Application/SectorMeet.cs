using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSectorMeets")]
    public class SectorMeet : FullAuditedEntity
    {
        [Column(TypeName = SectorMeetConsts.CodeType)]
        public string Code { get; set; }

        [Column(TypeName = SectorMeetConsts.YearType)]
        public int Year { get; set; }

        [Column(TypeName = SectorMeetConsts.CountType)]
        public int Count { get; set; }

        [Column(TypeName = SectorMeetConsts.GenerationType)]
        public bool Generation { get; set; }

        [Column(TypeName = SectorMeetConsts.MeetNameType)]
        public string MeetName { get; set; }

        [Column(TypeName = SectorMeetConsts.TerritorialUnitIdType)]
        [ForeignKey("TerritorialUnit")]
        public int TerritorialUnitId { get; set; }
        public TerritorialUnit TerritorialUnit { get; set; }

        [Column(TypeName = SectorMeetConsts.SocialConflictIdType)]
        [ForeignKey("SocialConflict")]
        public int? SocialConflictId { get; set; }
        public SocialConflict SocialConflict { get; set; }

        public List<SectorMeetSession> Sessions { get; set; }
    }
}
