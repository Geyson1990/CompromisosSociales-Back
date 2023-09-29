using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppFacts")]
    public class Fact : FullAuditedEntity
    {
        [Column(TypeName = FactConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = FactConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}
