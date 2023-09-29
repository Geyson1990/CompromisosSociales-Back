using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contable.Application
{
    [Table("AppCompromiseInvolved")]
    public class CompromiseInvolved : FullAuditedEntity
    {
        [Column(TypeName = CompromiseInvolvedConsts.CompromiseIdType)]
        [ForeignKey("Compromise")]
        public long CompromiseId { get; set; }
        public Compromise Compromise { get; set; }

        [Column(TypeName = CompromiseInvolvedConsts.ResponsibleActorIdType)]
        [ForeignKey("ResponsibleActor")]
        public int ResponsibleActorId { get; set; }
        public ResponsibleActor ResponsibleActor { get; set; }

        [Column(TypeName = CompromiseInvolvedConsts.ResponsibleSubActorIdType)]
        [ForeignKey("ResponsibleSubActor")]
        public int? ResponsibleSubActorId { get; set; }
        public ResponsibleSubActor ResponsibleSubActor { get; set; }
    }
}
