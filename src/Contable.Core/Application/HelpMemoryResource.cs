using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppHelpMemoryResources")]
    public class HelpMemoryResource : FullAuditedEntity
    {
        [Column(TypeName = HelpMemoryResourceConsts.HelpMemoryIdType)]
        [ForeignKey("HelpMemory")]
        public int HelpMemoryId { get; set; }
        public HelpMemory HelpMemory { get; set; }

        [Column(TypeName = HelpMemoryResourceConsts.AssetType)]
        public string CommonFolder { get; set; }

        [Column(TypeName = HelpMemoryResourceConsts.AssetType)]
        public string ResourceFolder { get; set; }

        [Column(TypeName = HelpMemoryResourceConsts.AssetType)]
        public string SectionFolder { get; set; }

        [Column(TypeName = HelpMemoryResourceConsts.AssetType)]
        public string FileName { get; set; }

        [Column(TypeName = HelpMemoryResourceConsts.AssetType)]
        public string Size { get; set; }

        [Column(TypeName = HelpMemoryResourceConsts.AssetType)]
        public string Extension { get; set; }

        [Column(TypeName = HelpMemoryResourceConsts.AssetType)]
        public string ClassName { get; set; }

        [Column(TypeName = HelpMemoryResourceConsts.AssetType)]
        public string Name { get; set; }

        [Column(TypeName = HelpMemoryResourceConsts.ResourceType)]
        public string Resource { get; set; }
    }
}
