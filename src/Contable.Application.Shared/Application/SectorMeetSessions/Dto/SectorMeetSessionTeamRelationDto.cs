using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SectorMeetSessions.Dto
{
    public class SectorMeetSessionTeamRelationDto : EntityDto
    {
        public string Document { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Job { get; set; }
        public string SecondSurname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public bool Remove { get; set; }
        public string Gender { get; set; }
        
    }
}
