using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppCrisisCommitteeAgreements")]
    public class CrisisCommitteeAgreement : FullAuditedEntity
    {
        [Column(TypeName = CrisisCommitteeAgreementConsts.CrisisCommitteeIdType)]
        [ForeignKey("CrisisCommittee")]
        public int CrisisCommitteeId { get; set; }
        public CrisisCommittee CrisisCommittee { get; set; }

        [Column(TypeName = CrisisCommitteeAgreementConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = CrisisCommitteeAgreementConsts.IndexType)]
        public int Index { get; set; }
    }
}
