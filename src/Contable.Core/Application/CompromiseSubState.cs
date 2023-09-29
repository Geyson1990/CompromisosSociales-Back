using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppCompromiseSubStates")]
    public class CompromiseSubState : FullAuditedEntity
    {
        [Column(TypeName = CompromiseSubStateConsts.CompromiseStateIdType)]
        [ForeignKey("CompromiseState")]
        public int CompromiseStateId { get; set; }
        public CompromiseState CompromiseState { get; set; }

        [Column(TypeName = CompromiseSubStateConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = CompromiseSubStateConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}
