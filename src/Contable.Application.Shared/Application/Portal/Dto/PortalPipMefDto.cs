using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Portal.Dto
{
    public class PortalPipMefDto
    {
        public decimal Total { get; set; }
        public string NumberText { get; set; }
        public int ProyectQuantity { get; set; }
        public List<PortalPipMefDataDto> Phases { get; set; }
    }
}
