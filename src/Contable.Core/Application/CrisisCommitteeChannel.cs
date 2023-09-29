using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppCrisisCommitteeChannels")]
    public class CrisisCommitteeChannel : FullAuditedEntity
    {
        [Column(TypeName = CrisisCommitteeChannelConsts.CrisisCommitteeIdType)]
        [ForeignKey("CrisisCommittee")]
        public int CrisisCommitteeId { get; set; }
        public CrisisCommittee CrisisCommittee { get; set; }

        [Column(TypeName = CrisisCommitteeChannelConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = CrisisCommitteeChannelConsts.IndexType)]
        public int Index { get; set; }
    }
}
