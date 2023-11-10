using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictAlerts.Dto
{
    public class SocialConflictAlertActorLocationDto : EntityDto
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string Job { get; set; }
        public string Community { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool IsPoliticalAssociation { get; set; }
        public string PoliticalAssociation { get; set; }
        public string Position { get; set; }
        public string Interest { get; set; }
        public SocialConflictAlertActorTypeDto ActorType { get; set; }
        public SocialConflictAlertActorMovementDto ActorMovement { get; set; }
        public int ActorId { get; set; }
        public bool Remove { get; set; }
    }
}
