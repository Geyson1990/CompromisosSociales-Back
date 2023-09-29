using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictNotes")]
    public class SocialConflictNote : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictNoteConsts.SocialConflictIdType)]
        [ForeignKey("SocialConflict")]
        public int SocialConflictId { get; set; }
        public SocialConflict SocialConflict { get; set; }

        [Column(TypeName = SocialConflictNoteConsts.DescriptionType)]
        public string Description { get; set; }
    }
}
