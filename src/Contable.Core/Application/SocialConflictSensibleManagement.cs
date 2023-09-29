using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictSensibleManagements")]
    public class SocialConflictSensibleManagement : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictSensibleManagementConsts.SocialConflictSensibleIdType)]
        [ForeignKey("SocialConflictSensible")]
        public int SocialConflictSensibleId { get; set; }
        public SocialConflictSensible SocialConflictSensible { get; set; }

        [Column(TypeName = SocialConflictSensibleManagementConsts.ManagementIdType)]
        [ForeignKey("Management")]
        public int ManagementId { get; set; }
        public Management Management { get; set; }

        [Column(TypeName = SocialConflictSensibleManagementConsts.ManagerIdType)]
        [ForeignKey("Manager")]
        public int ManagerId { get; set; }
        public Person Manager { get; set; }

        [Column(TypeName = SocialConflictSensibleManagementConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = SocialConflictSensibleManagementConsts.ManagementTimeType)]
        public DateTime ManagementTime { get; set; }

        [Column(TypeName = SocialConflictSensibleManagementConsts.CivilMenType)]
        public int? CivilMen { get; set; }

        [Column(TypeName = SocialConflictSensibleManagementConsts.CivilWomenType)]
        public int? CivilWomen { get; set; }

        [Column(TypeName = SocialConflictSensibleManagementConsts.StateMenType)]
        public int? StateMen { get; set; }

        [Column(TypeName = SocialConflictSensibleManagementConsts.StateWomenType)]
        public int? StateWomen { get; set; }

        [Column(TypeName = SocialConflictSensibleManagementConsts.CompanyMenType)]
        public int? CompanyMen { get; set; }

        [Column(TypeName = SocialConflictSensibleManagementConsts.CompanyWomenType)]
        public int? CompanyWomen { get; set; }

        [Column(TypeName = SocialConflictSensibleManagementConsts.VerificationType)]
        public bool Verification { get; set; }

        public List<SocialConflictSensibleManagementResource> Resources { get; set; }
    }
}
