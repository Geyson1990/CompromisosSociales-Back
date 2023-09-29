using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppResponsibleSubActors")]
    public class ResponsibleSubActor : FullAuditedEntity
    {
        public ResponsibleActor ResponsibleActor { get; set; }

        [Column(TypeName = ResponsibleSubActorConsts.NameType)]
        public string Name { get; set; }

    }
}
