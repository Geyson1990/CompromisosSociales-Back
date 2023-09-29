using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppAlertDemands")]
    public class AlertDemand : FullAuditedEntity
    {
        [Column(TypeName = AlertDemandConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = AlertDemandConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}
