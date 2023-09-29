using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppActorTypes")]
    public class ActorType : FullAuditedEntity
    {
        [Column(TypeName = ActorTypeConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = ActorTypeConsts.EnabledType)]
        public bool Enabled { get; set; }

        [Column(TypeName = ActorTypeConsts.ShowDetailType)]
        public bool ShowDetail { get; set; }

        [Column(TypeName = ActorTypeConsts.ShowMovementType)]
        public bool ShowMovement { get; set; }
    }
}
