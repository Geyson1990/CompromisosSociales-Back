using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Portal.Dto
{
    public class PortalSocialConflictLocationDto
    {
        public EntityDto TerritorialUnit { get; set; }
        public EntityDto Department { get; set; }
        public EntityDto Province { get; set; }
        public EntityDto District { get; set; }
    }
}
