using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSectorMeetSessionActions")]
    public class SectorMeetSessionAction : FullAuditedEntity
    {
        [Column(TypeName = SectorMeetSessionActionConsts.SectorMeetSessionIdType)]
        [ForeignKey("SectorMeetSession")]
        public int SectorMeetSessionId { get; set; }
        public SectorMeetSession SectorMeetSession { get; set; }

        [Column(TypeName = SectorMeetSessionActionConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = SectorMeetSessionActionConsts.IndexType)]
        public int Index { get; set; }
    }
}
