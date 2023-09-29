using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictAlertResources")]
    public class SocialConflictAlertResource : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictAlertResourceConsts.SocialConflictAlertIdType)]
        [ForeignKey("SocialConflictAlert")]
        public int SocialConflictAlertId { get; set; }
        public SocialConflictAlert SocialConflictAlert { get; set; }

        [Column(TypeName = SocialConflictAlertResourceConsts.AssetType)]
        public string CommonFolder { get; set; }

        [Column(TypeName = SocialConflictAlertResourceConsts.AssetType)]
        public string ResourceFolder { get; set; }

        [Column(TypeName = SocialConflictAlertResourceConsts.AssetType)]
        public string SectionFolder { get; set; }

        [Column(TypeName = SocialConflictAlertResourceConsts.AssetType)]
        public string FileName { get; set; }

        [Column(TypeName = SocialConflictAlertResourceConsts.AssetType)]
        public string Size { get; set; }

        [Column(TypeName = SocialConflictAlertResourceConsts.AssetType)]
        public string Extension { get; set; }

        [Column(TypeName = SocialConflictAlertResourceConsts.AssetType)]
        public string ClassName { get; set; }

        [Column(TypeName = SocialConflictAlertResourceConsts.AssetType)]
        public string Name { get; set; }

        [Column(TypeName = SocialConflictAlertResourceConsts.ResourceType)]
        public string Resource { get; set; }
    }
}
