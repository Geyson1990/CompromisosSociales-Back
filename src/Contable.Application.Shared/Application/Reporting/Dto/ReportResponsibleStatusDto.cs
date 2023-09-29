using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Reporting.Dto
{
    public class ReportResponsibleStatusDto
    {
        public string Name { get; set; }        
        public CompromiseType Type { get; set; }
        public int Total { get; set; }
        public int Compliments { get; set; }
    }
}

