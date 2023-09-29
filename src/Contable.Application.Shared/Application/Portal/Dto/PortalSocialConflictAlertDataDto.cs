using Contable.Application.Reporting.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Portal.Dto
{
    public class PortalSocialConflictAlertDataDto
    {
        public List<ReportSocialConflictAlertRiskDto> Risks { get; set; }
        public List<ReportSocialConflictAlertSectorDto> Sectors { get; set; }
        public List<ReportSocialConflictAlertStateDto> States { get; set; }
        public List<ReportSocialConflictAlertLocationDto> TerritorialUnits { get; set; }
        public List<ReportSocialConflictAlertTypologyDto> Typologies { get; set; }
    }
}
