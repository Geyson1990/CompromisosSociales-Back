using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictAlerts.Dto
{
    public class SocialConflictAlertRiskLocationDto : EntityDto
    {
        public DateTime? CreationTime { get; set; }
        public SocialConflictAlertRiskDto AlertRisk { get; set; }
        public DateTime RiskTime { get; set; }
        public string Description { get; set; }
        public string Observation { get; set; }
        public bool Remove { get; set; }
    }
}
