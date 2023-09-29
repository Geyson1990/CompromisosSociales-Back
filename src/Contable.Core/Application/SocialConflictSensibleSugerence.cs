using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictSensibleSugerences")]
    public class SocialConflictSensibleSugerence : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictSensibleSugerenceConsts.SocialConflictSensibleIdType)]
        [ForeignKey("SocialConflictSensible")]
        public int SocialConflictSensibleId { get; set; }
        public SocialConflictSensible SocialConflictSensible { get; set; }

        [Column(TypeName = SocialConflictSensibleSugerenceConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = SocialConflictSensibleSugerenceConsts.AcceptedType)]
        public bool Accepted { get; set; }

        [Column(TypeName = SocialConflictSensibleSugerenceConsts.AcceptTimeType)]
        public DateTime? AcceptTime { get; set; }

        [Column(TypeName = SocialConflictSensibleSugerenceConsts.AcceptedUserIdType)]
        public long? AcceptedUserId { get; set; }
    }
}
