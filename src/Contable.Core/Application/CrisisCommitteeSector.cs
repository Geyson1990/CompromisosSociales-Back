using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppCrisisCommitteeSectors")]
    public class CrisisCommitteeSector : FullAuditedEntity
    {
        [Column(TypeName = CrisisCommitteeSectorConsts.CrisisCommitteeIdType)]
        [ForeignKey("CrisisCommittee")]
        public int CrisisCommitteeId { get; set; }
        public CrisisCommittee CrisisCommittee { get; set; }

        [Column(TypeName = CrisisCommitteeSectorConsts.DirectoryGovernmentIdType)]
        [ForeignKey("DirectoryGovernment")]
        public int DirectoryGovernmentId { get; set; }
        public DirectoryGovernment DirectoryGovernment { get; set; }

        [Column(TypeName = CrisisCommitteeSectorConsts.IndexType)]
        public int Index { get; set; }
    }
}