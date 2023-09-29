using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppCompromiseStates")]
    public class CompromiseState : FullAuditedEntity
    {
        [Column(TypeName = CompromiseStateConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = CompromiseStateConsts.EnabledType)]
        public bool Enabled { get; set; }

        public List<CompromiseSubState> SubStates { get; set; }
    }
}
