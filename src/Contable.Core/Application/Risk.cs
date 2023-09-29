using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppRisks")]
    public class Risk : FullAuditedEntity
    {
        [Column(TypeName = RiskConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = RiskConsts.ColorType)]
        public string Color { get; set; }

        [Column(TypeName = RiskConsts.IndexType)]
        public int Index { get; set; }

        [Column(TypeName = RiskConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}
