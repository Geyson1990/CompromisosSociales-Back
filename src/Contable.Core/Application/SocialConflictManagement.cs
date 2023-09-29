using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictManagements")]
    public class SocialConflictManagement : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictManagementConsts.SocialConflictIdType)]
        [ForeignKey("SocialConflict")]
        public int SocialConflictId { get; set; }
        public SocialConflict SocialConflict { get; set; }

        [Column(TypeName = SocialConflictManagementConsts.ManagementIdType)]
        [ForeignKey("Management")]
        public int ManagementId { get; set; }
        public Management Management { get; set; }

        [Column(TypeName = SocialConflictManagementConsts.ManagerIdType)]
        [ForeignKey("Manager")]
        public int ManagerId { get; set; }
        public Person Manager { get; set; }

        [Column(TypeName = SocialConflictManagementConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = SocialConflictManagementConsts.ManagementTimeType)]
        public DateTime ManagementTime { get; set; }

        [Column(TypeName = SocialConflictManagementConsts.CivilMenType)]
        public int? CivilMen { get; set; }

        [Column(TypeName = SocialConflictManagementConsts.CivilWomenType)]
        public int? CivilWomen { get; set; }

        [Column(TypeName = SocialConflictManagementConsts.StateMenType)]
        public int? StateMen { get; set; }

        [Column(TypeName = SocialConflictManagementConsts.StateWomenType)]
        public int? StateWomen { get; set; }

        [Column(TypeName = SocialConflictManagementConsts.CompanyMenType)]
        public int? CompanyMen { get; set; }

        [Column(TypeName = SocialConflictManagementConsts.CompanyWomenType)]
        public int? CompanyWomen { get; set; }

        [Column(TypeName = SocialConflictManagementConsts.VerificationType)]
        public bool Verification { get; set; }

        public List<SocialConflictManagementResource> Resources { get; set; }
    }
}
