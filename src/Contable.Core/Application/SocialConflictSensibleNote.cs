using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictSensibleNotes")]
    public class SocialConflictSensibleNote : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictSensibleNoteConsts.SocialConflictSensibleIdType)]
        [ForeignKey("SocialConflictSensible")]
        public int SocialConflictSensibleId { get; set; }
        public SocialConflictSensible SocialConflictSensible { get; set; }

        [Column(TypeName = SocialConflictSensibleNoteConsts.DescriptionType)]
        public string Description { get; set; }
    }
}
