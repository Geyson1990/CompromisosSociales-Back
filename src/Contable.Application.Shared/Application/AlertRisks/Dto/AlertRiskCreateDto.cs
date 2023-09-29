using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.AlertRisks.Dto
{
    public class AlertRiskCreateDto
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int Index { get; set; }
        public bool Enabled { get; set; }
    }
}
