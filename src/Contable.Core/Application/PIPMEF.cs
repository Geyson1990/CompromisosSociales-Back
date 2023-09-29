using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contable.Application
{
    [Table("AppPIPMEF")]
    public class PIPMEF : FullAuditedEntity<long>
    {
        [Column(TypeName = PIPMEFConsts.IsOkType)]
        public bool IsOk { get; set; }

        [Column(TypeName = PIPMEFConsts.LastUpdateMEFType)]
        public DateTime? LastUpdateMEF { get; set; }

        [Column(TypeName = PIPMEFConsts.UnifiedCodeType)]
        public string UnifiedCode { get; set; }

        [Column(TypeName = PIPMEFConsts.SNIPCodeType)]
        public string SNIPCode { get; set; }

        #region Solo lectura      
        
        [Column(TypeName = PIPMEFConsts.ProjectNameType)]
        public string ProjectName { get; set; }

        [Column(TypeName = PIPMEFConsts.ViabilityDateType)]
        public string ViabilityDate { get; set; }

        [Column(TypeName = PIPMEFConsts.AccruedType)]
        public double Accrued { get; set; }

        [Column(TypeName = PIPMEFConsts.AccumulatedAccruedType)]
        public double AccumulatedAccrued { get; set; }

        [Column(TypeName = PIPMEFConsts.PIMType)]
        public decimal PIM { get; set; }

        [Column(TypeName = PIPMEFConsts.PIAType)]
        public decimal PIA { get; set; }

        [Column(TypeName = PIPMEFConsts.FormulatingUnitType)]
        public string FormulatingUnit { get; set; }

        [Column(TypeName = PIPMEFConsts.ExecutingUnitType)]
        public string ExecutingUnit { get; set; }

        [Column(TypeName = PIPMEFConsts.UpdatedCostType)]
        public decimal UpdatedCost { get; set; }

        [Column(TypeName = PIPMEFConsts.StatusType)]
        public string Status { get; set; }

        #endregion

        #region Seguimiento     

        [ForeignKey("PIPPhase")]
        public int? PIPPhaseId { get; set; }
        public Parameter PIPPhase { get; set; }

        [ForeignKey("PIPMilestone")]
        public int? PIPMilestoneId { get; set; }
        public Parameter PIPMilestone { get; set; }

        #endregion

        public List<Compromise> Compromises { get; set; }
        public List<Order> Orders { get; set; }
    }
}
