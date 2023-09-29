using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictManagementResources")]
    public class SocialConflictManagementResource : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictManagementResourceConsts.SocialConflictManagementIdType)]
        public int SocialConflictManagementId { get; set; }
        public SocialConflictManagement SocialConflictManagement { get; set; }
        
        [Column(TypeName = SocialConflictManagementResourceConsts.AssetType)]
        public string CommonFolder { get; set; }

        [Column(TypeName = SocialConflictManagementResourceConsts.AssetType)]
        public string ResourceFolder { get; set; }

        [Column(TypeName = SocialConflictManagementResourceConsts.AssetType)]
        public string SectionFolder { get; set; }

        [Column(TypeName = SocialConflictManagementResourceConsts.AssetType)]
        public string FileName { get; set; }

        [Column(TypeName = SocialConflictManagementResourceConsts.AssetType)]
        public string Size { get; set; }

        [Column(TypeName = SocialConflictManagementResourceConsts.AssetType)]
        public string Extension { get; set; }

        [Column(TypeName = SocialConflictManagementResourceConsts.AssetType)]
        public string ClassName { get; set; }

        [Column(TypeName = SocialConflictManagementResourceConsts.AssetType)]
        public string Name { get; set; }               

        [Column(TypeName = SocialConflictManagementResourceConsts.ResourceType)]
        public string Resource { get; set; }
    }
}
