using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppProspectiveRiskHistories")]
    public class ProspectiveRiskHistory : FullAuditedEntity
    {
        [ForeignKey("ProspectiveRisk")]
        [Column(TypeName = ProspectiveRiskHistoryConsts.ProspectiveRiskIdType)]
        public int ProspectiveRiskId { get; set; }
        public ProspectiveRisk ProspectiveRisk { get; set; }

        [Column(TypeName = ProspectiveRiskHistoryConsts.EvaluatedTimeType)]
        public DateTime EvaluatedTime { get; set; }

        [Column(TypeName = ProspectiveRiskHistoryConsts.WeightType)]
        public decimal Weight { get; set; }

        [Column(TypeName = ProspectiveRiskHistoryConsts.FixValueType)]
        public decimal FixValue { get; set; }

        [Column(TypeName = ProspectiveRiskHistoryConsts.ValueType)]
        public decimal Value { get; set; }

    }
}
