using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Reporting.Dto
{
    public class ReportResponsibleDto
    {
        public string Name { get; set; }
        public CompromiseType Type { get; set; }
        public int Count { get; set; }
    }   
}
