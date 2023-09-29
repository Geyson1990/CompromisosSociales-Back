using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppCrisisCommitteePlans")]
    public class CrisisCommitteePlan : FullAuditedEntity
    {
        [Column(TypeName = CrisisCommitteePlanConsts.CrisisCommitteeIdType)]
        [ForeignKey("CrisisCommittee")]
        public int CrisisCommitteeId { get; set; }
        public CrisisCommittee CrisisCommittee { get; set; }

        [Column(TypeName = CrisisCommitteePlanConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = CrisisCommitteePlanConsts.IndexType)]
        public int Index { get; set; }
    }
}
