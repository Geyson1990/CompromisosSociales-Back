using Contable.Application.Uploaders.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictAlerts.Dto
{
    public class SocialConflictAlertCreateDto
    {
        public SocialConflictAlertConflictDto SocialConflict { get; set; }
        public string Description { get; set; }
        public string Information { get; set; }
        public DateTime? AlertTime { get; set; }

        public string Demand { get; set; }
        public SocialConflictAlertDemandDto AlertDemand { get; set; }
        public SocialConflictAlertTypologyDto Typology { get; set; }
        public SocialConflictAlertSubTypologyDto SubTypology { get; set; }
        public SocialConflictAlertResponsibleDto AlertResponsible { get; set; }
        public SocialConflictAlertPersonDto Analyst { get; set; }
        public SocialConflictAlertPersonDto Manager { get; set; }
        public SocialConflictAlertPersonDto Coordinator { get; set; }
        public string AditionalInformation { get; set; }
        public string Source { get; set; }
        public string SourceType { get; set; }
        public string Link { get; set; }
        public string Recommendations { get; set; }
        public string Actions { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool Published { get; set; }

        public SocialConflictAlertTerritorialUnitDto TerritorialUnit { get; set; }
        public List<SocialConflictAlertActorLocationDto> Actors { get; set; }
        public List<SocialConflictAlertLocationDto> Locations { get; set; }
        public List<SocialConflictAlertRiskLocationDto> Risks { get; set; }
        public List<SocialConflictAlertSectorLocationDto> Sectors { get; set; }
        public List<SocialConflictAlertStateLocationDto> States { get; set; }
        public List<SocialConflictAlertSealLocationDto> Seals { get; set; }
        public List<UploadResourceInputDto> UploadFiles { get; set; }
        public List<SocialConflictAlertResourceDto> Resources { get; set; }
    }
}
