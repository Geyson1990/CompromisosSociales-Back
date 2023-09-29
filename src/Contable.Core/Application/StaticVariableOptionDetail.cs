using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppStaticVariableOptionDetails")]
    public class StaticVariableOptionDetail : FullAuditedEntity
    {
        [Column(TypeName = StaticVariableOptionDetailConsts.StaticVariableIdType)]
        public int StaticVariableId { get; set; }
        public StaticVariable StaticVariable { get; set; }

        [Column(TypeName = StaticVariableOptionDetailConsts.StaticVariableOptionIdType)]
        public int StaticVariableOptionId { get; set; }
        public StaticVariableOption StaticVariableOption { get; set; }

        [Column(TypeName = StaticVariableOptionDetailConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = StaticVariableOptionDetailConsts.IndexType)]
        public int Index { get; set; }

        [Column(TypeName = StaticVariableOptionDetailConsts.ValueType)]
        public decimal Value { get; set; }
    }
}
