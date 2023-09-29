using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSectorMeetSessionSummaries")]
    public class SectorMeetSessionSummary : FullAuditedEntity
    {
        [Column(TypeName = SectorMeetSessionSummaryConsts.SectorMeetSessionIdType)]
        [ForeignKey("SectorMeetSession")]
        public int SectorMeetSessionId { get; set; }
        public SectorMeetSession SectorMeetSession { get; set; }

        [Column(TypeName = SectorMeetSessionSummaryConsts.SectorMeetSessionLeaderIdType)]
        [ForeignKey("SectorMeetSessionLeader")]
        public int? SectorMeetSessionLeaderId { get; set; }
        public SectorMeetSessionLeader SectorMeetSessionLeader { get; set; }

        [Column(TypeName = SectorMeetSessionSummaryConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = SectorMeetSessionSummaryConsts.IndexType)]
        public int Index { get; set; }
    }
}
