using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictAlerts.Dto
{
    public class SocialConflictAlertGetDataDto
    {
        public SocialConflictAlertGetDto SocialConflictAlert { get; set; }
        public List<SocialConflictAlertDepartmentDto> Departments { get; set; }
        public List<SocialConflictAlertTerritorialUnitDto> TerritorialUnits { get; set; }
        public List<SocialConflictAlertRiskDto> Risks { get; set; }
        public List<SocialConflictAlertSectorDto> Sectors { get; set; }
        public List<SocialConflictAlertSealDto> Seals { get; set; }
        public List<SocialConflictAlertActorTypeDto> ActorTypes { get; set; }
        public List<SocialConflictAlertActorMovementDto> ActorMovements { get; set; }
        public List<SocialConflictAlertPersonDto> Persons { get; set; }
        public List<SocialConflictAlertTypologyDto> Typologies { get; set; }
        public List<SocialConflictAlertDemandDto> Demands { get; set; }
        public List<SocialConflictAlertResponsibleDto> Responsibles { get; set; }
    }
}
