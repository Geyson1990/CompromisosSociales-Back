using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictAlerts.Dto
{
    public class SocialConflictAlertGetAllDto : EntityDto
    {
        public SocialConflictAlertConflictDto SocialConflict { get; set; }
        public int Year { get; set; }
        public int Count { get; set; }
        public bool Generation { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Information { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime AlertTime { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool Published { get; set; }
        public SocialConflictAlertRiskLocationGetAllDto Risk { get; set; }
        public SocialConflictAlertAnalystGetAllDto Analyst { get; set; }
        public SocialConflictAlertResponsibleGetAllDto AlertResponsible { get; set; }
        public SocialConflictAlertTerritorialUnitDto TerritorialUnit { get; set; }
        public List<SocialConflictAlertLocationDto> Locations { get; set; }
        public List<SocialConflictAlertActorGetAllDto> Actors { get; set; }
    }
}
