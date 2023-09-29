using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppHelpMemories")]
    public class HelpMemory : FullAuditedEntity
    {
        [Column(TypeName = HelpMemoryConsts.SocialConflictIdType)]
        [ForeignKey("SocialConflict")]
        public int? SocialConflictId { get; set; }
        public SocialConflict SocialConflict { get; set; }

        [Column(TypeName = HelpMemoryConsts.SocialConflictSensibleIdType)]
        [ForeignKey("SocialConflictSensible")]
        public int? SocialConflictSensibleId { get; set; }
        public SocialConflictSensible SocialConflictSensible { get; set; }

        [Column(TypeName = HelpMemoryConsts.DirectoryGovernmentIdType)]
        [ForeignKey("DirectoryGovernment")]
        public int DirectoryGovernmentId { get; set; }
        public DirectoryGovernment DirectoryGovernment { get; set; }

        [Column(TypeName = HelpMemoryConsts.CodeType)]
        public string Code { get; set; }

        [Column(TypeName = HelpMemoryConsts.YearType)]
        public int Year { get; set; }

        [Column(TypeName = HelpMemoryConsts.CountType)]
        public int Count { get; set; }

        [Column(TypeName = HelpMemoryConsts.GenerationType)]
        public bool Generation { get; set; }

        [Column(TypeName = HelpMemoryConsts.SiteType)]
        public ConflictSite Site { get; set; }

        [Column(TypeName = HelpMemoryConsts.RequestType)]
        public string Request { get; set; }

        [Column(TypeName = HelpMemoryConsts.RequestTimeType)]
        public DateTime RequestTime { get; set; }

        public List<HelpMemoryResource> Resources { get; set; }
    }
}
