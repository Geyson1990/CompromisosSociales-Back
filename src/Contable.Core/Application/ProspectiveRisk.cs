using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppProspectiveRisks")]
    public class ProspectiveRisk : FullAuditedEntity
    {
        [Column(TypeName = ProspectiveRiskConsts.ProvinceIdType)]
        public int ProvinceId { get; set; }
        public Province Province { get; set; }

        [Column(TypeName = ProspectiveRiskConsts.EvaluatedTimeType)]
        public DateTime? EvaluatedTime { get; set; }

        [Column(TypeName = ProspectiveRiskConsts.FixRateType)]
        public decimal FixRate { get; set; }

        [Column(TypeName = ProspectiveRiskConsts.ValueType)]
        public decimal Value { get; set; }

        public List<ProspectiveRiskHistory> ProspectiveRiskHistories { get; set; }
    }
}
