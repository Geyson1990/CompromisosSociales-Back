using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppCompromiseLabels")]
    public class CompromiseLabel : FullAuditedEntity
    {
        [Column(TypeName = CompromiseLabelConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = CompromiseLabelConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}
