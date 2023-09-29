using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppCrisisCommitteeTasks")]
    public class CrisisCommitteeTask : FullAuditedEntity
    {
        [Column(TypeName = CrisisCommitteeTaskConsts.CrisisCommitteeIdType)]
        [ForeignKey("CrisisCommittee")]
        public int CrisisCommitteeId { get; set; }
        public CrisisCommittee CrisisCommittee { get; set; }

        [Column(TypeName = CrisisCommitteeTaskConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = CrisisCommitteeTaskConsts.IndexType)]
        public int Index { get; set; }
    }
}
