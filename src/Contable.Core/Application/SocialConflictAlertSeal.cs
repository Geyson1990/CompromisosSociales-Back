using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictAlertSeals")]
    public class SocialConflictAlertSeal : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictAlertSealConsts.SocialConflictAlertIdType)]
        [ForeignKey("SocialConflictAlert")]
        public int SocialConflictAlertId { get; set; }
        public SocialConflictAlert SocialConflictAlert { get; set; }

        [Column(TypeName = SocialConflictAlertSealConsts.AlertSealIdType)]
        [ForeignKey("AlertSeal")]
        public int AlertSealId { get; set; }
        public AlertSeal AlertSeal { get; set; }

        [Column(TypeName = SocialConflictAlertSealConsts.SealTimeType)]
        public DateTime SealTime { get; set; }

        [Column(TypeName = SocialConflictAlertSealConsts.DescriptionType)]
        public string Description { get; set; }

    }
}
