using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppCrisisCommitteeTeams")]
    public class CrisisCommitteeTeam : FullAuditedEntity
    {
        [Column(TypeName = CrisisCommitteeTeamConsts.CrisisCommitteeIdType)]
        [ForeignKey("CrisisCommittee")]
        public int CrisisCommitteeId { get; set; }
        public CrisisCommittee CrisisCommittee { get; set; }

        [Column(TypeName = CrisisCommitteeTeamConsts.AlertResponsibleIdType)]
        [ForeignKey("AlertResponsible")]
        public int AlertResponsibleId { get; set; }
        public AlertResponsible AlertResponsible { get; set; }

        [Column(TypeName = CrisisCommitteeTeamConsts.CrisisCommitteeJobIdType)]
        [ForeignKey("CrisisCommitteeJob")]
        public int CrisisCommitteeJobId { get; set; }
        public CrisisCommitteeJob CrisisCommitteeJob { get; set; }

        [Column(TypeName = CrisisCommitteeTeamConsts.JobType)]
        public string Job { get; set; }

        [Column(TypeName = CrisisCommitteeTeamConsts.DocumentType)]
        public string Document { get; set; }

        [Column(TypeName = CrisisCommitteeTeamConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = CrisisCommitteeTeamConsts.SurnameType)]
        public string Surname { get; set; }

        [Column(TypeName = CrisisCommitteeTeamConsts.SecondSurnameType)]
        public string SecondSurname { get; set; }

        [Column(TypeName = CrisisCommitteeTeamConsts.IndexType)]
        public int Index { get; set; }
    }
}
