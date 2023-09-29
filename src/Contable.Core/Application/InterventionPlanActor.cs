using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppInterventionPlanActors")]
    public class InterventionPlanActor : FullAuditedEntity
    {
        [Column(TypeName = InterventionPlanActorConsts.InterventionPlanIdType)]
        [ForeignKey("InterventionPlan")]
        public int InterventionPlanId { get; set; }
        public InterventionPlan InterventionPlan { get; set; }

        [Column(TypeName = InterventionPlanActorConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = InterventionPlanActorConsts.DocumentType)]
        public string Document { get; set; }

        [Column(TypeName = InterventionPlanActorConsts.JobType)]
        public string Job { get; set; }

        [Column(TypeName = InterventionPlanActorConsts.CommunityType)]
        public string Community { get; set; }

        [Column(TypeName = InterventionPlanActorConsts.PhoneNumberType)]
        public string PhoneNumber { get; set; }

        [Column(TypeName = InterventionPlanActorConsts.EmailAddressType)]
        public string EmailAddress { get; set; }

        [Column(TypeName = InterventionPlanActorConsts.IsPoliticalAssociationType)]
        public bool IsPoliticalAssociation { get; set; }

        [Column(TypeName = InterventionPlanActorConsts.PoliticalAssociationType)]
        public string PoliticalAssociation { get; set; }

        [Column(TypeName = InterventionPlanActorConsts.PositionType)]
        public string Position { get; set; }

        [Column(TypeName = InterventionPlanActorConsts.InterestType)]
        public string Interest { get; set; }

        [Column(TypeName = InterventionPlanActorConsts.ImportedType)]
        public bool Imported { get; set; }

        [Column(TypeName = InterventionPlanActorConsts.ImportedIdType)]
        public int ImportedId { get; set; }        

        [Column(TypeName = InterventionPlanActorConsts.ActorTypeIdType)]
        [ForeignKey("ActorType")]
        public int ActorTypeId { get; set; }
        public ActorType ActorType { get; set; }

        [Column(TypeName = InterventionPlanActorConsts.ActorMovementIdType)]
        [ForeignKey("ActorMovement")]
        public int? ActorMovementId { get; set; }
        public ActorMovement ActorMovement { get; set; }
    }
}
