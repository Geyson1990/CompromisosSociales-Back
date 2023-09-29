using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.CrisisCommittees.Dto
{
    public class CrisisCommitteeGetDataDto
    {
        public CrisisCommitteeGetDto CrisisCommittee { get; set; }
        public List<CrisisCommitteeAlertResponsibleRelationDto> AlertResponsibles { get; set; }
        public List<CrisisCommitteeJobRelationDto> Jobs { get; set; }
        public List<CrisisCommitteePersonRelationDto> Persons { get; set; }
    }
}
