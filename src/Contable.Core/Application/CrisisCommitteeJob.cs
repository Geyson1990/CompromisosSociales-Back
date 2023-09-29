using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppCrisisCommitteeJobs")]
    public class CrisisCommitteeJob : FullAuditedEntity
    {
        [Column(TypeName = CrisisCommitteeJobConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = CrisisCommitteeJobConsts.EnabledType)]
        public bool Enabled { get; set; }

        [Column(TypeName = CrisisCommitteeJobConsts.ShowDescriptionType)]
        public bool ShowDescription { get; set; }
    }
}
