using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.AlertSectors.Dto
{
    public class AlertSectorCreateDto
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public bool Enabled { get; set; }
    }
}
