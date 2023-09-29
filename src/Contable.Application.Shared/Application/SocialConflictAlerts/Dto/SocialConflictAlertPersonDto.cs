using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictAlerts.Dto
{
    public class SocialConflictAlertPersonDto : EntityDto
    {
        public string Name { get; set; }
        public PersonType Type { get; set; }
    }
}
