using Abp.Application.Services.Dto;
using Contable.Application.MeetsResponsibles.Dto;
using Contable.Application.SectorMeets.Dto;
using System;
using System.Collections.Generic;

namespace Contable.Application.Meets.Dto
{
    public class MeetGetDataDto
    {
        public MeetGetDto Meet { get; set; }
        public List<MeetParticipantsGetDto> MeetsParticipants { get; set; }
    }
}
