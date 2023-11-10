using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictActors")]
    public class SocialConflictActor : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictActorConsts.SocialConflictIdType)]
        [ForeignKey("SocialConflict")]
        public int? SocialConflictId { get; set; }
        public SocialConflict SocialConflict { get; set; }

        [Column(TypeName = SocialConflictActorConsts.SocialConflictActorIdType)]
        [ForeignKey("Actors")]
        public int? ActorId { get; set; }
        public Actor Actor { get; set; }

        [Column(TypeName = SocialConflictActorConsts.SocialConflictAlertIdType)]
        [ForeignKey("SocialConflictAlert")]
        public int? SocialConflictAlertId { get; set; }
        public SocialConflictAlert SocialConflictAlert { get; set; }

        [Column(TypeName = SocialConflictActorConsts.SocialConflictSensibleIdType)]
        [ForeignKey("SocialConflictSensible")]
        public int? SocialConflictSensibleId { get; set; }
        public SocialConflictSensible SocialConflictSensible { get; set; }

        [Column(TypeName = SocialConflictActorConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = SocialConflictActorConsts.DocumentType)]
        public string Document { get; set; }

        [Column(TypeName = SocialConflictActorConsts.JobType)]
        public string Job { get; set; }

        [Column(TypeName = SocialConflictActorConsts.CommunityType)]
        public string Community { get; set; }

        [Column(TypeName = SocialConflictActorConsts.PhoneNumberType)]
        public string PhoneNumber { get; set; }

        [Column(TypeName = SocialConflictActorConsts.EmailAddressType)]
        public string EmailAddress { get; set; }

        [Column(TypeName = SocialConflictActorConsts.IsPoliticalAssociationType)]
        public bool IsPoliticalAssociation { get; set; }

        [Column(TypeName = SocialConflictActorConsts.PoliticalAssociationType)]
        public string PoliticalAssociation { get; set; }

        [Column(TypeName = SocialConflictActorConsts.PositionType)]
        public string Position { get; set; }

        [Column(TypeName = SocialConflictActorConsts.InterestType)]
        public string Interest { get; set; }

        [Column(TypeName = SocialConflictActorConsts.SiteType)]
        public ActorSite Site { get; set; }

        [Column(TypeName = SocialConflictActorConsts.ActorTypeIdType)]
        [ForeignKey("ActorType")]
        public int ActorTypeId { get; set; }
        public ActorType ActorType { get; set; }

        [Column(TypeName = SocialConflictActorConsts.ActorMovementIdType)]
        [ForeignKey("ActorMovement")]
        public int? ActorMovementId { get; set; }
        public ActorMovement ActorMovement { get; set; }
    }
}
