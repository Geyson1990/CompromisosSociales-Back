using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppInterventionPlanActivities")]
    public class InterventionPlanActivity : FullAuditedEntity
    {
        [Column(TypeName = InterventionPlanActivityConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = InterventionPlanActivityConsts.EnabledType)]
        public bool Enabled { get; set; }

        [Column(TypeName = InterventionPlanActivityConsts.ShowDescriptionType)]
        public bool ShowDescription { get; set; }
    }
}
