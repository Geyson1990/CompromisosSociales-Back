using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictAlerts.Dto
{
    public class SocialConflictAlertConflictDto : EntityDto
    {
        public string Code { get; set; }
        public string CaseName { get; set; }
    }
}
