using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Reports.Dto
{
    public class JasperException
    {
        public bool Success { get; set; }
        public JasperExeptionMessage Error { get; set; }
    }
}
