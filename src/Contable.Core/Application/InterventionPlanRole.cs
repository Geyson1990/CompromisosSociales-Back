using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppInterventionPlanRoles")]
    public class InterventionPlanRole : FullAuditedEntity
    {
        [Column(TypeName = InterventionPlanRoleConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = InterventionPlanRoleConsts.EnabledType)]
        public bool Enabled { get; set; }

        [Column(TypeName = InterventionPlanRoleConsts.ShowDescriptionType)] 
        public bool ShowDescription { get; set; }
    }
}
