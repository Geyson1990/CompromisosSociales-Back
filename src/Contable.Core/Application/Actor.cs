using Abp.Domain.Entities.Auditing;
using Contable.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppActors")]
    public class Actor : FullAuditedEntity
    {
                [Column(TypeName = ActorConsts.FullNameType)]
        public string FullName { get; set; }

        [Column(TypeName = ActorConsts.DocumentNumberType)]
        public string DocumentNumber { get; set; }

        [Column(TypeName = ActorConsts.JobPositionType)]
        public string JobPosition { get; set; }

        [Column(TypeName = ActorConsts.InstitutionType)]
        public string Institution { get; set; }

        [Column(TypeName = ActorConsts.InstitutionAddressType)]
        public string InstitutionAddress { get; set; }

        [Column(TypeName = ActorConsts.PhoneNumberType)]
        public string PhoneNumber { get; set; }

        [Column(TypeName = ActorConsts.EmailAddressType)]
        public string EmailAddress { get; set; }

        [Column(TypeName = ActorConsts.IsPoliticalAssociationType)]
        public bool IsPoliticalAssociation { get; set; }

        [Column(TypeName = ActorConsts.PoliticalAssociationType)]
        public string PoliticalAssociation { get; set; }

        [Column(TypeName = ActorConsts.PositionType)]
        public string Position { get; set; }

        [Column(TypeName = ActorConsts.InterestType)]
        public string Interest { get; set; }

        [Column(TypeName = ActorConsts.EnabledType)]
        public bool Enabled { get; set; }

        [Column(TypeName = ActorConsts.ActorTypeIdType)]
        [ForeignKey("ActorType")]
        public int ActorTypeId { get; set; }
        public ActorType ActorType { get; set; }

        [Column(TypeName = ActorConsts.ActorMovementIdType)]
        [ForeignKey("ActorMovement")]
        public int? ActorMovementId { get; set; }
        public ActorMovement ActorMovement { get; set; }

    }
}
