using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppPersonTypes")]
    public class ActorPersonType : FullAuditedEntity
    {
        [Column(TypeName = ActorPersonTypeConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = ActorPersonTypeConsts.EnabledType)]
        public bool Enabled { get; set; }

    }
}