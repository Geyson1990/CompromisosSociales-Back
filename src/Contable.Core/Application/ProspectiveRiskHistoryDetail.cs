using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppProspectiveRiskHistoryDetails")]
    public class ProspectiveRiskHistoryDetail : FullAuditedEntity
    {
        [ForeignKey("ProspectiveRiskHistory")]
        [Column(TypeName = ProspectiveRiskHistoryDetailConsts.ProspectiveRiskHistoryIdType)]
        public int ProspectiveRiskHistoryId { get; set; }
        public ProspectiveRiskHistory ProspectiveRiskHistory { get; set; }

        [ForeignKey("StaticVariable")]
        [Column(TypeName = ProspectiveRiskHistoryDetailConsts.StaticVariableIdType)]
        public int StaticVariableId { get; set; }
        public StaticVariable StaticVariable { get; set; }

        [ForeignKey("StaticVariableOption")]
        [Column(TypeName = ProspectiveRiskHistoryDetailConsts.StaticVariableOptionIdType)]
        public int StaticVariableOptionId { get; set; }
        public StaticVariableOption StaticVariableOption { get; set; }

        [Column(TypeName = ProspectiveRiskHistoryDetailConsts.WeightType)]
        public decimal Weight { get; set; }

        [Column(TypeName = ProspectiveRiskHistoryDetailConsts.ValueType)]
        public decimal Value { get; set; }
    }
}
