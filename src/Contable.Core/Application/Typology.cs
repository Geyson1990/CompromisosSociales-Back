using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppTypologies")]
    public class Typology : FullAuditedEntity
    {
        [Column(TypeName = TypologyConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = TypologyConsts.EnabledType)]
        public bool Enabled { get; set; }

        public List<SubTypology> SubTypologies { get; set; }
    }
}

