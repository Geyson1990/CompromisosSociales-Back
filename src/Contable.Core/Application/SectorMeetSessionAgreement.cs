using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSectorMeetSessionAgreements")]
    public class SectorMeetSessionAgreement : FullAuditedEntity
    {
        [Column(TypeName = SectorMeetSessionAgreementConsts.SectorMeetSessionIdType)]
        [ForeignKey("SectorMeetSession")]
        public int SectorMeetSessionId { get; set; }
        public SectorMeetSession SectorMeetSession { get; set; }

        [Column(TypeName = SectorMeetSessionAgreementConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = SectorMeetSessionAgreementConsts.IndexType)]
        public int Index { get; set; }
    }
}
