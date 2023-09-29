using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.CrisisCommittees.Dto
{
    public class CrisisCommitteeTeamRelationDto : EntityDto
    {
        public CrisisCommitteeAlertResponsibleRelationDto AlertResponsible { get; set; }
        public CrisisCommitteeJobRelationDto CrisisCommitteeJob { get; set; }
        public string Job { get; set; }
        public string Document { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondSurname { get; set; }
        public bool Remove { get; set; }
    }
}
