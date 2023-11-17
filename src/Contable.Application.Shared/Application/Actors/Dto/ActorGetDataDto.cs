using System;
using System.Collections.Generic;
using System.Text;
using Contable.Application.Managers.Dto;
using Contable.Application.SocialConflicts.Dto;
using Contable.Application.SocialConflictAlerts.Dto;
using Contable.Application.SocialConflictSensibles.Dto;
namespace Contable.Application.Actors.Dto
{
    public class ActorGetDataDto
    {
        public ActorGetDto Actor { get; set; }
        public List<ActorTypeDto> ActorTypes { get; set; }
        public List<ActorMovementDto> ActorMovements { get; set; }
        public List<SocialConflictGetAllDto> SocialConflicts { get; set; }
        public List<SocialConflictAlertGetAllDto> SocialConflictAlerts { get; set; }
        public List<SocialConflictSensibleGetAllDto> SocialConflictSensibles { get; set; }
    }
}
