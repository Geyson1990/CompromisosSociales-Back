using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppStaticVariables")]
    public class StaticVariable : FullAuditedEntity
    {
        [Column(TypeName = StaticVariableConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = StaticVariableConsts.FamilyType)]
        public StaticVariableFamily Family { get; set; }

        [Column(TypeName = StaticVariableConsts.EnabledType)]
        public bool Enabled { get; set; }

        [Column(TypeName = StaticVariableConsts.WeightType)]
        public decimal Weight { get; set; }

        public List<StaticVariableOption> Options { get; set; }
        public List<StaticVariableOptionDetail> Details { get; set; }
    }
}
