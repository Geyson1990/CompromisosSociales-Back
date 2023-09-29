using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Reporting.Dto
{
    public class ReportSummaryStatusSplitDto
    {
        public string Name { get; set; }
        public List<ReportSummaryCountStatusDto> OpenStatus { get; set; }
        public List<ReportSummaryCountStatusDto> CloseStatus { get; set; }
    }

    public class ReportSummaryCountStatusDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int Count { get; set; }
    }
}
