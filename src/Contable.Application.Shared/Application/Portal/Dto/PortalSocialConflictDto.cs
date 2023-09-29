using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Portal.Dto
{
    public class PortalSocialConflictDto : EntityDto
    {
        public DateTime? LastModificationTime { get; set; }
        public DateTime CreationTime { get; set; }
        public string Code { get; set; }
        public string CaseName { get; set; }
        public string Description { get; set; }
        public string Dialog { get; set; }
        public List<PortalSocialConflictLocationDto> Locations { get; set; }
    }
}
