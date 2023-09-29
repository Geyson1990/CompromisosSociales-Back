using Contable.Application.Reporting.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Portal.Dto
{
    public class PortalGetAllDto
    {
        public List<ReportSummaryJoinDto> Summary { get; set; }
        public List<ReportStatusDto> Status { get; set; }
        public List<ReportStatusDto> OpenStatus { get; set; }
        public List<ReportResponsibleJoinDto> Responsibles { get; set; }
    }
}
