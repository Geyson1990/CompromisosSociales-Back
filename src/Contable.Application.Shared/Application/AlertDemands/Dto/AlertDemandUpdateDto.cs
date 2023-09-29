using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.AlertDemands.Dto
{
    public class AlertDemandUpdateDto : EntityDto
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public bool Enabled { get; set; }
    }
}
