using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictGeoDto : EntityDto
    {
        public string CaseName { get; set; }
        public string Description { get; set; }
        public string TypologyName { get; set; }
        public string SubTypologyName { get; set; }
        public string SectorName { get; set; }
        public string GovernmentLevelName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public List<SocialConflictLocationGeoDto> Locations { get; set; }
    }
}
