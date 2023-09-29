using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDinamicVariableDetails")]
    public class DinamicVariableDetail : FullAuditedEntity
    {
        [Column(TypeName = DinamicVariableConsts.IdType)]
        [ForeignKey("Province")]
        public int ProvinceId { get; set; }
        public Province Province { get; set; }

        [Column(TypeName = DinamicVariableConsts.IdType)]
        [ForeignKey("DinamicVariable")]
        public int DinamicVariableId { get; set; }
        public DinamicVariable DinamicVariable { get; set; }

        [Column(TypeName = DinamicVariableConsts.ValueType)]
        public decimal Value { get; set; }
    }
}
