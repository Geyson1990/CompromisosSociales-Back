using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppInterventionPlanOptions")]
    public class InterventionPlanOption : FullAuditedEntity
    {
        [Column(TypeName = InterventionPlanOptionConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = InterventionPlanOptionConsts.EnabledType)]
        public bool Enabled { get; set; }

        [Column(TypeName = InterventionPlanOptionConsts.IndexType)]
        public int Index { get; set; }
    }
}
