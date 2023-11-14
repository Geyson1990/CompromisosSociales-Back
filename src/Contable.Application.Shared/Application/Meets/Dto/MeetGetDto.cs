using Abp.Application.Services.Dto;
using Contable.Application.MeetsResponsibles.Dto;
using System;
using System.Collections.Generic;

namespace Contable.Application.Meets.Dto
{
    public class MeetGetDto
    {
        public bool ReplaceCode { get; set; }
        public int ReplaceYear { get; set; }
        public int ReplaceCount { get; set; }
        public string MeetName { get; set; }       
        public EntityDto SocialConflict { get; set; }

        public int ModalityId { get; set; }

        public int TypeId { get; set; }

        public int PlaceId { get; set; }
        public int LevelRiskId { get; set; }
        public DateTime DayHour { get; set; }

        public string Objet { get; set; }
        public int ResponsibleId { get; set; }
        public string ResponsibleName { get; set; }
        public int RolId { get; set; }
        public string FactorRisk { get; set; }
        public List<MeetParticipantsGetDto> Participants { get; set; }
    }
}
