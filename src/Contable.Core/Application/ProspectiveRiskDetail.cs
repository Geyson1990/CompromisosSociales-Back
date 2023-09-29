using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppProspectiveRiskDetails")]
    public class ProspectiveRiskDetail : FullAuditedEntity
    {
        [Column(TypeName = ProspectiveRiskDetailConsts.ProspectiveRiskIdType)]
        public int ProspectiveRiskId { get; set; }
        public ProspectiveRisk ProspectiveRisk { get; set; }

        [Column(TypeName = ProspectiveRiskDetailConsts.StaticVariableOptionIdType)]
        public int StaticVariableOptionId { get; set; }
        public StaticVariableOption StaticVariableOption { get; set; }

        [Column(TypeName = ProspectiveRiskDetailConsts.EnabledType)]
        public bool Enabled { get; set; }

        [Column(TypeName = ProspectiveRiskDetailConsts.ValueType)]
        public decimal Value { get; set; }
    }
}
