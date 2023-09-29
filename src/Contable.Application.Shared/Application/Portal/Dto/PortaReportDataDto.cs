using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Portal.Dto
{
    public class PortaReportDataDto
    {
        public List<PortalTerritorialUnitDto> TerritorialUnits { get; set; }
        public List<PortalDepartmentDto> Departments { get; set; }
        public List<PortalSocialConflictDto> SocialConflicts { get; set; }
        public List<PortalRiskDto> Risks { get; set; }
        public List<PortalGeographicDto> Geographics { get; set; }
        public List<PortalTypologyDto> Typologies { get; set; }
    }
}
