using Abp.Application.Services.Dto;
using Contable.Application.Parameters.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.External.Dto
{
    public class PIPMEFDto : EntityDto<long>
    {
        public bool IsOk { get; set; }
        public DateTime? LastUpdateMEF { get; set; }

        public string UnifiedCode { get; set; }
        public string SNIPCode { get; set; }
        public ParameterDto PIPPhase { get; set; }
        public ParameterDto PIPMilestone { get; set; }

        public decimal UpdatedCost { get; set; }
        public string ProjectName { get; set; }
        public string ViabilityDate { get; set; }
        public decimal Accrued { get; set; }
        public decimal AccumulatedAccrued { get; set; }
        public decimal PIM { get; set; }
        public decimal PIA { get; set; }
        public string FormulatingUnit { get; set; }
        public string Status { get; set; }
        public string ExecutingUnit { get; set; }
    }
}
