using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictSensibleVerificationHistories")]
    public class SocialConflictSensibleVerificationHistory : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictSensibleVerificationHistoryConsts.SocialConflictSensibleIdType)]
        [ForeignKey("SocialConflictSensible")]
        public int SocialConflictSensibleId { get; set; }
        public SocialConflictSensible SocialConflictSensible { get; set; }

        [Column(TypeName = SocialConflictSensibleVerificationHistoryConsts.SiteType)]
        public SocialConflictVerificationSite Site { get; set; }

        [Column(TypeName = SocialConflictSensibleVerificationHistoryConsts.OldStateType)]
        public bool OldState { get; set; }

        [Column(TypeName = SocialConflictSensibleVerificationHistoryConsts.NewStateType)]
        public bool NewState { get; set; }

        [Column(TypeName = SocialConflictSensibleVerificationHistoryConsts.EntityIdType)]
        public int? EntityId { get; set; }
    }
}
