using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppAlertSectors")]
    public class AlertSector : FullAuditedEntity
    {
        [Column(TypeName = AlertSectorConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = AlertSectorConsts.IndexType)]
        public int Index { get; set; }

        [Column(TypeName = AlertSectorConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}
