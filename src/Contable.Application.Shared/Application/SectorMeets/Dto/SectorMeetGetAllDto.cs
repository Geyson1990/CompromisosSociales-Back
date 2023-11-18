using Abp.Application.Services.Dto;
using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application.SectorMeets.Dto
{
    public class SectorMeetGetAllDto : EntityDto
    {
        public DateTime CreationTime { get; set; }
        public string Code { get; set; }
        public int Year { get; set; }
        public int Count { get; set; }
        public string MeetName { get; set; }
        public SectorMeetTerritorialUnitRelationDto TerritorialUnit { get; set; }
        public SectorMeetSocialConflictRelationDto SocialConflict { get; set; }
        public int? State { get; set; }
    }
}

