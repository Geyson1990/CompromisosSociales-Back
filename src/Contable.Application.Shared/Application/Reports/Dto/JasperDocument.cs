using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Reports.Dto
{
    public class JasperDocument
    {
        public byte[] Report { get; set; }
        public bool Success { get; set; }
        public JasperException Exception { get; set; }
    }
}
