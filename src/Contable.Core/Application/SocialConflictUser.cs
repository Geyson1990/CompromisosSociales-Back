using Abp.Domain.Entities.Auditing;
using Contable.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictUsers")]
    public class SocialConflictUser : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictUserConsts.SocialConflictIdType)]
        [ForeignKey("SocialConflict")]
        public int SocialConflictId { get; set; }
        public SocialConflict SocialConflict { get; set; }

        [Column(TypeName = SocialConflictUserConsts.UserIdType)]
        [ForeignKey("User")]
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
