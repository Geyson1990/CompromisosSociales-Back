using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppAlertResponsibles")]
    public class AlertResponsible : FullAuditedEntity
    {
        [Column(TypeName = AlertResponsibleConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = AlertResponsibleConsts.ShortNameType)]
        public string ShortName { get; set; }

        [Column(TypeName = AlertResponsibleConsts.TracingType)]
        public bool Tracing { get; set; }

        [Column(TypeName = AlertResponsibleConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}
