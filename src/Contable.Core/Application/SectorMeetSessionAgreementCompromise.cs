using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contable.Application
{
    [Table("AppSectorMeetSessionAgreementsCompromises")]
    public class SectorMeetSessionAgreementCompromise : FullAuditedEntity
    {
        [Column(TypeName = SectorMeetSessionAgreementCompromiseConsts.SectorMeetSessionAgreementIdType)]
        [ForeignKey("SectorMeetSessionAgreement")]
        public int SectorMeetSessionAgreementId { get; set; }
        public SectorMeetSessionAgreement SectorMeetSessionAgreement { get; set; }


        [Column(TypeName = CompromiseConsts.TypeLong)]
        [ForeignKey("Compromise")]
        public long CompromiseId { get; set; }
        public Compromise Compromise { get; set; }

        [Column(TypeName = SectorMeetSessionAgreementCompromiseConsts.IndexType)]
        public int StatusId { get; set; }

       
    }
}
