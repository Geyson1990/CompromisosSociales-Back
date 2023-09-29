using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSectorMeetSessionCriticalAspects")]
    public class SectorMeetSessionCriticalAspect : FullAuditedEntity
    {
        [Column(TypeName = SectorMeetSessionCriticalAspectConsts.SectorMeetSessionIdType)]
        [ForeignKey("SectorMeetSession")]
        public int SectorMeetSessionId { get; set; }
        public SectorMeetSession SectorMeetSession { get; set; }

        [Column(TypeName = SectorMeetSessionCriticalAspectConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = SectorMeetSessionCriticalAspectConsts.IndexType)]
        public int Index { get; set; }
    }
}
