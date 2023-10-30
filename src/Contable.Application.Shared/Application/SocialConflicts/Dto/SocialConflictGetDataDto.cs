using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictGetDataDto
    {
        public SocialConflictGetDto SocialConflict { get; set; }
        public List<SocialConflictDepartmentDto> Departments { get; set; }
        public List<SocialConflictTerritorialUnitDto> TerritorialUnits { get; set; }
        public List<SocialConflictPersonDto> Persons { get; set; }
        public List<SocialConflictTypologyDto> Typologies { get; set; }
        public List<SocialConflictFactDto> Facts { get; set; }
        public List<SocialConflictActorTypeDto> ActorTypes { get; set; }
        public List<SocialConflictActorMovementDto> ActorMovements { get; set; }
        public List<SocialConflictSectorDto> Sectors { get; set; }
        public List<SocialConflictRiskDto> Risks { get; set; }
        public List<SocialConflictManagementDto> Managements { get; set; }
    }


    public class SocialConflictCompromisesGetDataDto
    {
        public SocialConflictGetDto SocialConflict { get; set; }  

    }

}
