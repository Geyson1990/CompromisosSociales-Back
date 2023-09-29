using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Reporting.Dto
{
    public class DashboardGetAllDto
    {
        public List<ReportStatusDto> Status { get; set; }
        public List<ReportStatusDto> OpenStatus { get; set; }
        public List<ReportStatusDto> CloseStatus { get; set; }
        public List<ReportSummaryStatusSplitDto> Summary { get; set; }
        public List<ReportParameterDto> StatusList { get; set; }
}
}
