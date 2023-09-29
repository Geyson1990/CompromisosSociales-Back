using Abp.Application.Services.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictAlerts.Dto
{
    public class SocialConflictAlertTypologyDto : EntityDto
    {
        public string Name { get; set; }
        public List<SocialConflictAlertSubTypologyDto> SubTypologies { get; set; }
    }
}
