using Abp.Domain.Entities.Auditing;
using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictTaskManagementExtends")]
    public class SocialConflictTaskManagementExtend : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictTaskManagementExtendConsts.SocialConflictTaskManagementIdType)]
        [ForeignKey("SocialConflictTaskManagement")]
        public int SocialConflictTaskManagementId { get; set; }
        public SocialConflictTaskManagement SocialConflictTaskManagement { get; set; }

        [Column(TypeName = SocialConflictTaskManagementExtendConsts.DescriptionExtendType)]
        public string Description { get; set; }

        [Column(TypeName = SocialConflictTaskManagementExtendConsts.DeadlineType)]
        public DateTime Deadline { get; set; }
    }
}
