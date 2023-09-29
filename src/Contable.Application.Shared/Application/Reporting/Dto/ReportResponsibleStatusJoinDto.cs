using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Reporting.Dto
{
    public class ReportResponsibleStatusJoinDto
    {
        public string Name { get; set; }        

        public int PipTotal { get; set; }
        public int PipCompliments { get; set; }

        public int ActivityTotal { get; set; }
        public int ActivityCompliments { get; set; }
    }
}

