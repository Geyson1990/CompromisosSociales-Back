using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictTaskManagementResources")]
    public class SocialConflictTaskManagementResource : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictTaskManagementResourceConsts.SocialConflictTaskManagementIdType)]
        [ForeignKey("SocialConflictTaskManagement")]
        public int SocialConflictTaskManagementId { get; set; }
        public SocialConflictTaskManagement SocialConflictTaskManagement { get; set; }

        [Column(TypeName = SocialConflictTaskManagementResourceConsts.AssetType)]
        public string CommonFolder { get; set; }

        [Column(TypeName = SocialConflictTaskManagementResourceConsts.AssetType)]
        public string ResourceFolder { get; set; }

        [Column(TypeName = SocialConflictTaskManagementResourceConsts.AssetType)]
        public string SectionFolder { get; set; }

        [Column(TypeName = SocialConflictTaskManagementResourceConsts.AssetType)]
        public string FileName { get; set; }

        [Column(TypeName = SocialConflictTaskManagementResourceConsts.AssetType)]
        public string Size { get; set; }

        [Column(TypeName = SocialConflictTaskManagementResourceConsts.AssetType)]
        public string Extension { get; set; }

        [Column(TypeName = SocialConflictTaskManagementResourceConsts.AssetType)]
        public string ClassName { get; set; }

        [Column(TypeName = SocialConflictTaskManagementResourceConsts.AssetType)]
        public string Name { get; set; }

        [Column(TypeName = SocialConflictTaskManagementResourceConsts.ResourceType)]
        public string Resource { get; set; }
    }
}
