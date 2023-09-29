using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppCrisisCommitteeMessage")]
    public class CrisisCommitteeMessage : FullAuditedEntity
    {
        [Column(TypeName = CrisisCommitteeMessageConsts.CrisisCommitteeIdType)]
        [ForeignKey("CrisisCommittee")]
        public int CrisisCommitteeId { get; set; }
        public CrisisCommittee CrisisCommittee { get; set; }

        [Column(TypeName = CrisisCommitteeMessageConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = CrisisCommitteeMessageConsts.IndexType)]
        public int Index { get; set; }
    }
}
