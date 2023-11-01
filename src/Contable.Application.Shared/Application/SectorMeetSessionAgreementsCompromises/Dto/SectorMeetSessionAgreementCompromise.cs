using Abp.Application.Services.Dto;
using Castle.MicroKernel.SubSystems.Conversion;
using Contable.Application.Compromises.Dto;
using Contable.Application.SectorMeetSessions.Dto;
using Contable.Authorization.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application.SectorMeetSessionsAgreement.Dto
{
    public class SectorMeetSessionAgreementCompromiseDto : EntityDto
    {
        public int SectorMeetSessionAgreementId { get; set; }   
        public long CompromiseId { get; set; }
        //public CompromiseGetDto Compromise { get; set; }
        //public SectorMeetSessionAgreementRelationDto SectorMeetSessionAgreement { get; set; }
        public int StatusId { get; set; }
    }
}
