using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Portal.Dto
{
    public class PortalGetAllSocialConflictSensibleInputDto : PortalGetAllInputDto
    {
        public int SocialConflictRiskId { get; set; }
        public int GeographicType { get; set; }
    }
}
