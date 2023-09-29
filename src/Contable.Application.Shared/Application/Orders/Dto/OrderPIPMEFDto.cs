using Abp.Application.Services.Dto;
using Contable.Application.Parameters.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Orders.Dto
{
    public class OrderPIPMEFDto : EntityDto<long>
    {
        public bool IsOk { get; set; }

        public string UnifiedCode { get; set; }
        public string SNIPCode { get; set; }
        public ParameterDto PIPPhase { get; set; }
        public ParameterDto PIPMilestone { get; set; }

        public string ProjectName { get; set; }
        public string ViabilityDate { get; set; }
        public string Accrued { get; set; }
        public string AccumulatedAccrued { get; set; }
        public string PIM { get; set; }
        public string PIA { get; set; }
        public string FormulatingUnit { get; set; }
        public string ExecutingUnit { get; set; }
    }
}
