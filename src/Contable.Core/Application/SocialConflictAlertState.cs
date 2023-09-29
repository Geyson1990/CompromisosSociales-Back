using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictAlertStates")]
    public class SocialConflictAlertState : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictAlertStateConsts.SocialConflictAlertIdType)]
        [ForeignKey("SocialConflictAlert")]
        public int SocialConflictAlertId { get; set; }
        public SocialConflictAlert SocialConflictAlert { get; set; }

        [Column(TypeName = SocialConflictAlertStateConsts.StateTimeType)]
        public DateTime StateTime { get; set; }

        [Column(TypeName = SocialConflictAlertStateConsts.DescriptionType)]
        public string Description { get; set; }
    }
}
