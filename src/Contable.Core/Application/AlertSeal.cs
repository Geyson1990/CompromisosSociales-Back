using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppAlertSeals")]
    public class AlertSeal : FullAuditedEntity
    {
        [Column(TypeName = AlertSealConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = AlertSealConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}
