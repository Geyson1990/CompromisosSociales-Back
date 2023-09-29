using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictAlerts.Dto
{
    public class SocialConflictAlertActorGetAllDto : EntityDto
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public string Community { get; set; }
        public string Position { get; set; }
        public string Interest { get; set; }
        public SocialConflictAlertActorTypeGetAllDto ActorType { get; set; }
        public SocialConflictAlertActorMovementGetAllDto ActorMovement { get; set; }
    }
}
