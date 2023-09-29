using Contable.Application.Reporting.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Portal.Dto
{
    public class PortalSocialConflictSensibleDataDto
    {
        public List<ReportSocialConflictSensibleRiskDto> Risks { get; set; }
        public List<ReportSocialConflictSensibleGeographycTypeDto> GeographycTypes { get; set; }
        public List<ReportSocialConflictSensibleLocationDto> Locations { get; set; }
    }
}
