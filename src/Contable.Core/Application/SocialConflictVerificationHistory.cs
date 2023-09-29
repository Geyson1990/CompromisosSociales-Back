using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictVerificationHistories")]
    public class SocialConflictVerificationHistory : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictVerificationHistoryConsts.SocialConflictIdType)]
        [ForeignKey("SocialConflict")]
        public int SocialConflictId { get; set; }
        public SocialConflict SocialConflict { get; set; }

        [Column(TypeName = SocialConflictVerificationHistoryConsts.SiteType)]
        public SocialConflictVerificationSite Site { get; set; }

        [Column(TypeName = SocialConflictVerificationHistoryConsts.OldStateType)]
        public bool OldState { get; set; }

        [Column(TypeName = SocialConflictVerificationHistoryConsts.NewStateType)]
        public bool NewState { get; set; }

        [Column(TypeName = SocialConflictVerificationHistoryConsts.EntityIdType)]
        public int? EntityId { get; set; }
    }
}
