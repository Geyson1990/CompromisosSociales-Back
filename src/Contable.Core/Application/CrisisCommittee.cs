using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppCrisisCommittees")]
    public class CrisisCommittee : FullAuditedEntity
    {
        [Column(TypeName = CrisisCommitteeConsts.InterventionPlanIdType)]
        [ForeignKey("InterventionPlan")]
        public int InterventionPlanId { get; set; }
        public InterventionPlan InterventionPlan { get; set; }

        [Column(TypeName = CrisisCommitteeConsts.CodeType)]
        public string Code { get; set; }

        [Column(TypeName = CrisisCommitteeConsts.YearType)]
        public int Year { get; set; }

        [Column(TypeName = CrisisCommitteeConsts.CountType)]
        public int Count { get; set; }

        [Column(TypeName = CrisisCommitteeConsts.GenerationType)]
        public bool Generation { get; set; }

        [Column(TypeName = CrisisCommitteeConsts.CrisisCommitteeTimeType)]
        public DateTime CrisisCommitteeTime { get; set; }

        [Column(TypeName = CrisisCommitteeConsts.CaseNameType)]
        public string CaseName { get; set; }

        [Column(TypeName = CrisisCommitteeConsts.PersonIdType)]
        [ForeignKey("Person")]
        public int? PersonId { get; set; }
        public Person Person { get; set; }

        public List<CrisisCommitteeTeam> Teams { get; set; }
        public List<CrisisCommitteePlan> Plans { get; set; }
        public List<CrisisCommitteeAction> Actions { get; set; }
        public List<CrisisCommitteeMessage> Messages { get; set; }
        public List<CrisisCommitteeChannel> Channels { get; set; }
        public List<CrisisCommitteeSector> Sectors { get; set; }
        public List<CrisisCommitteeTask> Tasks { get; set; }
        public List<CrisisCommitteeAgreement> Agreements { get; set; }
    }
}
