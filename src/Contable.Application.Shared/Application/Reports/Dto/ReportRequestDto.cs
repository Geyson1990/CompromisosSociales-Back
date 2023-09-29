using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Reports.Dto
{
    public class ReportRequestDto
    {
        public string ReportName { get; set; }
        public ReportType ReportType { get; set; }
        public string FileName { get; set; }
        public List<JasperReportParameter> Parameters { get; set; }
    }
}
