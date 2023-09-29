using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppStaticVariableOptions")]
    public class StaticVariableOption : FullAuditedEntity
    {
        [Column(TypeName = StaticVariableOptionConsts.StaticVariableIdType)]
        public int StaticVariableId { get; set; }
        public StaticVariable StaticVariable { get; set; }

        [Column(TypeName = StaticVariableOptionConsts.DinamicVariableIdType)]
        public int? DinamicVariableId { get; set; }
        public DinamicVariable DinamicVariable { get; set; }

        [Column(TypeName = StaticVariableOptionConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = StaticVariableOptionConsts.IndexType)]
        public int Index { get; set; }

        [Column(TypeName = StaticVariableOptionConsts.EnabledType)]
        public bool Enabled { get; set; }

        [Column(TypeName = StaticVariableOptionConsts.Type)]
        public StaticVariableType Type { get; set; }

        [Column(TypeName = StaticVariableOptionConsts.SiteType)]
        public StaticVariableSite Site { get; set; }

        [Column(TypeName = StaticVariableOptionConsts.ValueType)]
        public decimal Value { get; set; }

        public List<StaticVariableOptionDetail> Details { get; set; }

        public List<ProspectiveRiskDetail> ProspectiveRiskDetails { get; set; }
    }
}
