using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contable.Application
{
    [Table("AppResponsibleActors")]
    public class ResponsibleActor : FullAuditedEntity
    {
        [Column(TypeName = ResponsibleActorConsts.ResponsibleTypeIdType)]
        [ForeignKey("ResponsibleType")]
        public int? ResponsibleTypeId { get; set; }
        public ResponsibleType ResponsibleType { get; set; }

        [Column(TypeName = ResponsibleActorConsts.ResponsibleSubTypeIdType)]
        [ForeignKey("ResponsibleSubType")]
        public int? ResponsibleSubTypeId { get; set; }
        public ResponsibleSubType ResponsibleSubType { get; set; }

        [Column(TypeName = ResponsibleActorConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = ResponsibleActorConsts.ResponsibleActorType)]
        public ResponsibleActorType Type { get; set; }

        public List<ResponsibleSubActor> ResponsibleSubActors { get; set; }

        public List<CompromiseInvolved> CompromiseInvolveds { get; set; }
    }
}
