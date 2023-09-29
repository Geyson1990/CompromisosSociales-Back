using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.PIPMef.Dto
{
    public class PortalPipMEFGroupDto
    {
        public long Id { get; set; }
        public DateTime? LastUpdateMEF { get; set; }
        public string UnifiedCode { get; set; }
        public string SNIPCode { get; set; }
        public string ProjectName { get; set; }
        public string ViabilityDate { get; set; }
        public double Accrued { get; set; }
        public double AccumulatedAccrued { get; set; }
        public decimal PIM { get; set; }
        public decimal PIA { get; set; }
        public string FormulatingUnit { get; set; }
        public string ExecutingUnit { get; set; }
        public decimal UpdatedCost { get; set; }
        public string Status { get; set; }
        public int? PIPPhaseId { get; set; }
        public int? PIPMilestoneId { get; set; }
    }
}
