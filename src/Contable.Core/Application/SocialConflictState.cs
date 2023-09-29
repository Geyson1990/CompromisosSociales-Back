using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictStates")]
    public class SocialConflictState : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictStateConsts.SocialConflictIdType)]
        [ForeignKey("SocialConflict")]
        public int SocialConflictId { get; set; }
        public SocialConflict SocialConflict { get; set; }

        [Column(TypeName = SocialConflictStateConsts.ManagerIdType)]
        [ForeignKey("Manager")]
        public int ManagerId { get; set; }
        public Person Manager { get; set; }

        [Column(TypeName = SocialConflictStateConsts.StateTimeType)]
        public DateTime StateTime { get; set; }

        [Column(TypeName = SocialConflictStateConsts.StateType)]
        public string State { get; set; }

        [Column(TypeName = SocialConflictStateConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = SocialConflictStateConsts.VerificationType)]
        public bool Verification { get; set; }
    }
}
