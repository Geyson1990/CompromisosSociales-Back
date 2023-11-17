using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contable.Application
{
    [Table("AppSectorMeetSessionRiskFactors")]
    public class SectorMeetSessionRiskFactors : FullAuditedEntity
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
