using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Portal.Dto
{
    public class PortalPipMefDataDto
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public long Count { get; set; }
        public decimal Total { get; set; }
        public ParameterStep Step { get; set; }
    }
}
