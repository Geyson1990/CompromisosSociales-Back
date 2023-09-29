using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictSugerences")]
    public class SocialConflictSugerence : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictSugerenceConsts.SocialConflictIdType)]
        [ForeignKey("SocialConflict")]
        public int SocialConflictId { get; set; }
        public SocialConflict SocialConflict { get; set; }

        [Column(TypeName = SocialConflictSugerenceConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = SocialConflictSugerenceConsts.AcceptedType)]
        public bool Accepted { get; set; }

        [Column(TypeName = SocialConflictSugerenceConsts.AcceptTimeType)]
        public DateTime? AcceptTime { get; set; }

        [Column(TypeName = SocialConflictSugerenceConsts.AcceptedUserIdType)]
        public long? AcceptedUserId { get; set; }
    }
}
