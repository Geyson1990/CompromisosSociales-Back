using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.AlertRisks.Dto
{
    public class AlertRiskGetDto : EntityDto
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int Index { get; set; }
        public bool Enabled { get; set; }
    }
}
