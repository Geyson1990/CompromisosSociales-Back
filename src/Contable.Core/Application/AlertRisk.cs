using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppAlertRisks")]
    public class AlertRisk : FullAuditedEntity
    {
        [Column(TypeName = AlertRiskConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = AlertRiskConsts.ColorType)]
        public string Color { get; set; }

        [Column(TypeName = AlertRiskConsts.IndexType)]
        public int Index { get; set; }

        [Column(TypeName = AlertRiskConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}
