using System.Collections.Generic;
using Contable.Application.Parameters.Dto;
using Contable.Application.ResponsibleActors.Dto;

namespace Contable.Application.Compromises.Dto
{
    public class CompromiseGetDataDto
    {
        public CompromiseGetDto Compromise { get; set; }          
        public List<ParameterDto> PIPPhases { get; set; }
        public List<ParameterDto> PIPMilestones { get; set; }
        public List<ParameterDto> Statuses { get; set; }
        public List<ResponsibleActorGetAllDto> ResponsibleActors { get; set; }
        public List<CompromiseResponsibleTypeDto> ResponsibleTypes { get; set; }
        public List<CompromiseLabelLocationDto> Labels { get; set; }
        public List<CompromiseStateDto> States { get; set; }
        public List<CompromiseTerritorialUnitDto> TerritorialUnits { get; set; }
    }
}
