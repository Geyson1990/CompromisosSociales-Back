using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppInterventionPlanEntities")]
    public class InterventionPlanEntity : FullAuditedEntity
    {
        [Column(TypeName = InterventionPlanEntityConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = InterventionPlanEntityConsts.EnabledType)]
        public bool Enabled { get; set; }

        [Column(TypeName = InterventionPlanEntityConsts.Type)]
        public InterventionPlanEntityType Type { get; set; }
    }
}
