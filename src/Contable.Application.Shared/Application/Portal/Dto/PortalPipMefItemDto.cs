using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Portal.Dto
{
    public class PortalPipMefItemDto
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public ParameterType Type { get; set; }
    }
}
