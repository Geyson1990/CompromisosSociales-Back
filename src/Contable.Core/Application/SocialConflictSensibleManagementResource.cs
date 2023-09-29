using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictSensibleManagementResources")]
    public class SocialConflictSensibleManagementResource : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictSensibleManagementResourceConsts.SocialConflictSensibleManagementIdType)]
        [ForeignKey("SocialConflictSensibleManagement")]
        public int SocialConflictSensibleManagementId { get; set; }
        public SocialConflictSensibleManagement SocialConflictSensibleManagement { get; set; }
        
        [Column(TypeName = SocialConflictSensibleManagementResourceConsts.AssetType)]
        public string CommonFolder { get; set; }

        [Column(TypeName = SocialConflictSensibleManagementResourceConsts.AssetType)]
        public string ResourceFolder { get; set; }

        [Column(TypeName = SocialConflictSensibleManagementResourceConsts.AssetType)]
        public string SectionFolder { get; set; }

        [Column(TypeName = SocialConflictSensibleManagementResourceConsts.AssetType)]
        public string FileName { get; set; }

        [Column(TypeName = SocialConflictSensibleManagementResourceConsts.AssetType)]
        public string Size { get; set; }

        [Column(TypeName = SocialConflictSensibleManagementResourceConsts.AssetType)]
        public string Extension { get; set; }

        [Column(TypeName = SocialConflictSensibleManagementResourceConsts.AssetType)]
        public string ClassName { get; set; }

        [Column(TypeName = SocialConflictSensibleManagementResourceConsts.AssetType)]
        public string Name { get; set; }               

        [Column(TypeName = SocialConflictSensibleManagementResourceConsts.ResourceType)]
        public string Resource { get; set; }
    }
}
