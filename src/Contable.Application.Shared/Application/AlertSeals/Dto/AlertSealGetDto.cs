using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.AlertSeals.Dto
{
    public class AlertSealGetDto : EntityDto
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
