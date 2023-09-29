using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSubTypologies")]
    public class SubTypology : FullAuditedEntity
    {
        [Column(TypeName = SubTypologyConsts.TypologyIdType)]
        [ForeignKey("Typology")]
        public int TypologyId { get; set; }
        public Typology Typology { get; set; }

        [Column(TypeName = SubTypologyConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = SubTypologyConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}
