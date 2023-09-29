using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictSensibles.Dto
{
    public class SocialConflictSensibleGetDataDto
    {
        public SocialConflictSensibleGetDto SocialConflictSensible { get; set; }
        public List<SocialConflictSensibleDepartmentDto> Departments { get; set; }
        public List<SocialConflictSensibleTerritorialUnitDto> TerritorialUnits { get; set; }
        public List<SocialConflictSensiblePersonDto> Persons { get; set; }
        public List<SocialConflictSensibleTypologyDto> Typologies { get; set; }
        public List<SocialConflictSensibleFactDto> Facts { get; set; }
        public List<SocialConflictSensibleActorTypeDto> ActorTypes { get; set; }
        public List<SocialConflictSensibleActorMovementDto> ActorMovements { get; set; }
        public List<SocialConflictSensibleRiskDto> Risks { get; set; }
        public List<SocialConflictSensibleManagementDto> Managements { get; set; }
    }
}
