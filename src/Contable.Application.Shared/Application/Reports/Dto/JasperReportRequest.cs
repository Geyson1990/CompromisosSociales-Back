using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Reports.Dto
{
    public class JasperReportRequest
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public List<JasperReportParameter> Parameters { get; set; } 
    }
}
