using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictResources")]
    public class SocialConflictResource : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictResourceConsts.SocialConflictIdType)]
        [ForeignKey("SocialConflict")]
        public int SocialConflictId { get; set; }
        public SocialConflict SocialConflict { get; set; }

        [Column(TypeName = SocialConflictResourceConsts.AssetType)]
        public string CommonFolder { get; set; }

        [Column(TypeName = SocialConflictResourceConsts.AssetType)]
        public string ResourceFolder { get; set; }

        [Column(TypeName = SocialConflictResourceConsts.AssetType)]
        public string SectionFolder { get; set; }

        [Column(TypeName = SocialConflictResourceConsts.AssetType)]
        public string FileName { get; set; }

        [Column(TypeName = SocialConflictResourceConsts.AssetType)]
        public string Size { get; set; }

        [Column(TypeName = SocialConflictResourceConsts.AssetType)]
        public string Extension { get; set; }

        [Column(TypeName = SocialConflictResourceConsts.AssetType)]
        public string ClassName { get; set; }

        [Column(TypeName = SocialConflictResourceConsts.AssetType)]
        public string Name { get; set; }

        [Column(TypeName = SocialConflictResourceConsts.ResourceType)]
        public string Resource { get; set; }
    }
}
