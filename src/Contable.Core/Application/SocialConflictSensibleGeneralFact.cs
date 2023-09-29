using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictSensibleGeneralFacts")]
    public class SocialConflictSensibleGeneralFact : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictSensibleGeneralFactConsts.SocialConflictSensibleIdType)]
        [ForeignKey("SocialConflictSensible")]
        public int SocialConflictSensibleId { get; set; }
        public SocialConflictSensible SocialConflictSensible { get; set; }

        [Column(TypeName = SocialConflictSensibleGeneralFactConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = SocialConflictSensibleGeneralFactConsts.FactTimeType)]
        public DateTime FactTime { get; set; }
    }
}
