using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictAlerts.Dto
{
    public class SocialConflictAlertStateLocationDto : EntityDto
    {
        public DateTime? CreationTime { get; set; }
        public DateTime StateTime { get; set; }
        public string Description { get; set; }
        public bool Remove { get; set; }
    }
}
