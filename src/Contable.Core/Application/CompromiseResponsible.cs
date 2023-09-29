using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contable.Application
{
    [Table("AppCompromiseResponsibles")]
    public class CompromiseResponsible : FullAuditedEntity
    {
        [Column(TypeName = CompromiseResponsibleConsts.CompromiseIdType)]
        [ForeignKey("Compromise")]
        public long CompromiseId { get; set; }
        public Compromise Compromise { get; set; }

        [Column(TypeName = CompromiseResponsibleConsts.ResponsibleActorIdType)]
        [ForeignKey("ResponsibleActor")]
        public int ResponsibleActorId { get; set; }
        public ResponsibleActor ResponsibleActor { get; set; }

        [Column(TypeName = CompromiseResponsibleConsts.ResponsibleSubActorIdType)]
        [ForeignKey("ResponsibleSubActor")]
        public int? ResponsibleSubActorId { get; set; }
        public ResponsibleSubActor ResponsibleSubActor { get; set; }
    }
}
