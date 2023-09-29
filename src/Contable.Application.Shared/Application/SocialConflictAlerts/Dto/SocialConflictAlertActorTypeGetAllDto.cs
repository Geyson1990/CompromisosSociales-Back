using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictAlerts.Dto
{
    public class SocialConflictAlertActorTypeGetAllDto : EntityDto
    {
        public string Name { get; set; }
        public bool ShowDetail { get; set; }
        public bool ShowMovement { get; set; }
    }
}
