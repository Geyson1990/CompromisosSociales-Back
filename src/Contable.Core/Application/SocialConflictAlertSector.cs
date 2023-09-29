using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictAlertSectors")]
    public class SocialConflictAlertSector : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictAlertSectorConsts.SocialConflictAlertIdType)]
        [ForeignKey("SocialConflictAlert")]
        public int SocialConflictAlertId { get; set; }
        public SocialConflictAlert SocialConflictAlert { get; set; }

        [Column(TypeName = SocialConflictAlertSectorConsts.AlertSectorIdType)]
        [ForeignKey("AlertSector")]
        public int AlertSectorId { get; set; }
        public AlertSector AlertSector { get; set; }

        [Column(TypeName = SocialConflictAlertSectorConsts.SectorTimeType)]
        public DateTime SectorTime { get; set; }

        [Column(TypeName = SocialConflictAlertSectorConsts.DescriptionType)]
        public string Description { get; set; }
    }
}
