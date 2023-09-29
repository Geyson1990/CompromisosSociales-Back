using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppInterventionPlanTeams")]
    public class InterventionPlanTeam : FullAuditedEntity
    {
        [Column(TypeName = InterventionPlanTeamConsts.InterventionPlanIdType)]
        [ForeignKey("InterventionPlan")]
        public int InterventionPlanId { get; set; }
        public InterventionPlan InterventionPlan { get; set; }

        [Column(TypeName = InterventionPlanTeamConsts.DocumentType)]
        public string Document { get; set; }

        [Column(TypeName = InterventionPlanTeamConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = InterventionPlanTeamConsts.SurnameType)]
        public string Surname { get; set; }

        [Column(TypeName = InterventionPlanTeamConsts.SecondSurnameType)]
        public string SecondSurname { get; set; }

        [Column(TypeName = InterventionPlanTeamConsts.InterventionPlanEntityIdType)]
        [ForeignKey("InterventionPlanEntity")]
        public int InterventionPlanEntityId { get; set; }
        public InterventionPlanEntity InterventionPlanEntity { get; set; }

        [Column(TypeName = InterventionPlanTeamConsts.AlertResponsibleIdType)]
        [ForeignKey("AlertResponsible")]
        public int? AlertResponsibleId { get; set; }
        public AlertResponsible AlertResponsible { get; set; }

        [Column(TypeName = InterventionPlanTeamConsts.DirectoryGovernmentIdType)]
        [ForeignKey("DirectoryGovernment")]
        public int? DirectoryGovernmentId { get; set; }
        public DirectoryGovernment DirectoryGovernment { get; set; }

        [Column(TypeName = InterventionPlanTeamConsts.EntityType)]
        public string Entity { get; set; }

        [Column(TypeName = InterventionPlanTeamConsts.JobType)]
        public string Job { get; set; }

        [Column(TypeName = InterventionPlanTeamConsts.InterventionPlanRoleIdType)]
        [ForeignKey("InterventionPlanRole")]
        public int InterventionPlanRoleId { get; set; }
        public InterventionPlanRole InterventionPlanRole { get; set; }

        [Column(TypeName = InterventionPlanTeamConsts.RoleType)]
        public string Role { get; set; }
    }
}
