using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictTaskManagements")]
    public class SocialConflictTaskManagement : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictTaskManagementConsts.SocialConflictIdType)]
        [ForeignKey("SocialConflict")]
        public int? SocialConflictId { get; set; }
        public SocialConflict SocialConflict { get; set; }

        [Column(TypeName = SocialConflictTaskManagementConsts.SocialConflictAlertIdType)]
        [ForeignKey("SocialConflictAlert")]
        public int? SocialConflictAlertId { get; set; }
        public SocialConflictAlert SocialConflictAlert { get; set; }

        [Column(TypeName = SocialConflictTaskManagementConsts.SocialConflictSensibleIdType)]
        [ForeignKey("SocialConflictSensible")]
        public int? SocialConflictSensibleId { get; set; }
        public SocialConflictSensible SocialConflictSensible { get; set; }

        [Column(TypeName = SocialConflictTaskManagementConsts.TitleType)]
        public string Title { get; set; }

        [Column(TypeName = SocialConflictTaskManagementConsts.DescriptionType)]
        public string Description { get; set; }   

        [Column(TypeName = SocialConflictTaskManagementConsts.StartTimeType)]
        public DateTime? StartTime { get; set; }

        [Column(TypeName = SocialConflictTaskManagementConsts.DeadlineType)]
        public DateTime? Deadline { get; set; }

        [Column(TypeName = SocialConflictTaskManagementConsts.StatusType)]
        public TaskStatus Status { get; set; }

        [Column(TypeName = SocialConflictTaskManagementConsts.StatusType)]
        public ConflictSite Site { get; set; }

        [Column(TypeName = SocialConflictTaskManagementConsts.SendedType)]
        public bool SendedCreation { get; set; }

        [Column(TypeName = SocialConflictTaskManagementConsts.SendedType)]
        public bool SendedDeadline { get; set; }

        public List<SocialConflictTaskManagementComment> Comments { get; set; }
        public List<SocialConflictTaskManagementPerson> Persons { get; set; }
        public List<SocialConflictTaskManagementExtend> Extends { get; set; }       
        public List<SocialConflictTaskManagementResource> Resources { get; set; }
        public List<SocialConflictTaskManagementHistory> Histories { get; set; }
    }
}
