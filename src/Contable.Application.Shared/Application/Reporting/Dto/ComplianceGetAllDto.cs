using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Reporting.Dto
{
    public class ComplianceGetAllDto
    {
        public List<ReportSummaryJoinDto> Summary { get; set; }
        public List<ReportStatusDto> Status { get; set; }
        public List<ReportStatusDto> OpenStatus { get; set; }        
        public List<ReportResponsibleStatusJoinDto> ResponsibleStatus { get; set; }
        public List<ReportStatusDto> PipOpenStatus { get; set; }
    }
}
