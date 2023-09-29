using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppCrisisCommitteeActions")]
    public class CrisisCommitteeAction : FullAuditedEntity
    {
        [Column(TypeName = CrisisCommitteeActionConsts.CrisisCommitteeIdType)]
        [ForeignKey("CrisisCommittee")]
        public int CrisisCommitteeId { get; set; }
        public CrisisCommittee CrisisCommittee { get; set; }

        [Column(TypeName = CrisisCommitteeActionConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = CrisisCommitteeActionConsts.IndexType)]
        public int Index { get; set; }
    }
}
