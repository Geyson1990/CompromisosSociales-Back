using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSectorMeetSessionSchedules")]
    public class SectorMeetSessionSchedule : FullAuditedEntity
    {
        [Column(TypeName = SectorMeetSessionScheduleConsts.SectorMeetSessionIdType)]
        [ForeignKey("SectorMeetSession")]
        public int SectorMeetSessionId { get; set; }
        public SectorMeetSession SectorMeetSession { get; set; }

        [Column(TypeName = SectorMeetSessionScheduleConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = SectorMeetSessionScheduleConsts.IndexType)]
        public int Index { get; set; }
    }
}
