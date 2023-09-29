using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictAlerts.Dto
{
    public class SocialConflictAlertRiskLocationGetAllDto : EntityDto
    {
        public SocialConflictAlertRiskGetAllDto AlertRisk { get; set; }
        public DateTime RiskTime { get; set; }
        public string Description { get; set; }
        public string Observation { get; set; }
    }
}
