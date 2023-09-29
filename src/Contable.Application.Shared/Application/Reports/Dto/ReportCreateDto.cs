using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Reports.Dto
{
    public class ReportCreateDto : EntityDto
    {
        public ReportType Type { get; set; }
    }
}
