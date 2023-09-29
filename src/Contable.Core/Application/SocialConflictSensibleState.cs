using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictSensibleStates")]
    public class SocialConflictSensibleState : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictSensibleStateConsts.SocialConflictSensibleIdType)]
        [ForeignKey("SocialConflictSensible")]
        public int SocialConflictSensibleId { get; set; }
        public SocialConflictSensible SocialConflictSensible { get; set; }

        [Column(TypeName = SocialConflictSensibleStateConsts.ManagerIdType)]
        [ForeignKey("Manager")]
        public int ManagerId { get; set; }
        public Person Manager { get; set; }

        [Column(TypeName = SocialConflictSensibleStateConsts.StateTimeType)]
        public DateTime StateTime { get; set; }

        [Column(TypeName = SocialConflictSensibleStateConsts.StateType)]
        public string State { get; set; }

        [Column(TypeName = SocialConflictSensibleStateConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = SocialConflictSensibleStateConsts.VerificationType)]
        public bool Verification { get; set; }
    }
}
