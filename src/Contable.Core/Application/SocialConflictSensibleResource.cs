using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictSensibleResources")]
    public class SocialConflictSensibleResource : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictSensibleResourceConsts.SocialConflictSensibleIdType)]
        [ForeignKey("SocialConflictSensible")]
        public int SocialConflictSensibleId { get; set; }
        public SocialConflictSensible SocialConflictSensible { get; set; }

        [Column(TypeName = SocialConflictSensibleResourceConsts.AssetType)]
        public string CommonFolder { get; set; }

        [Column(TypeName = SocialConflictSensibleResourceConsts.AssetType)]
        public string ResourceFolder { get; set; }

        [Column(TypeName = SocialConflictSensibleResourceConsts.AssetType)]
        public string SectionFolder { get; set; }

        [Column(TypeName = SocialConflictSensibleResourceConsts.AssetType)]
        public string FileName { get; set; }

        [Column(TypeName = SocialConflictSensibleResourceConsts.AssetType)]
        public string Size { get; set; }

        [Column(TypeName = SocialConflictSensibleResourceConsts.AssetType)]
        public string Extension { get; set; }

        [Column(TypeName = SocialConflictSensibleResourceConsts.AssetType)]
        public string ClassName { get; set; }

        [Column(TypeName = SocialConflictSensibleResourceConsts.AssetType)]
        public string Name { get; set; }

        [Column(TypeName = SocialConflictSensibleResourceConsts.ResourceType)]
        public string Resource { get; set; }
    }
}
