using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contable.Application
{
    [Table("AppSituations")]
    public class Situation : FullAuditedEntity<long>
    {
        public Compromise Compromise  { get; set;}

        [Column(TypeName = SituationConsts.DescriptionType)]
        public string Description { get; set; }

        public SituationResource Resource { get; set; }
    }
}
