using Contable.Application.Reporting.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Portal.Dto
{
    public class PortalSocialConflictDataDto
    {
        public List<ReportSocialConflictRiskDto> Risks { get; set; }
        public List<ReportSocialConflictGeographycTypeDto> GeographycTypes { get; set; }
        public List<ReportSocialConflictLocationDto> Locations { get; set; }
    }
}
