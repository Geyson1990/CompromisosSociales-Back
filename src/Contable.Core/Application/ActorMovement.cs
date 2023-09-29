using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppActorMovements")]
    public class ActorMovement : FullAuditedEntity
    {
        [Column(TypeName = ActorMovementConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = ActorMovementConsts.IndexType)]
        public int Index { get; set; }

        [Column(TypeName = ActorMovementConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}
