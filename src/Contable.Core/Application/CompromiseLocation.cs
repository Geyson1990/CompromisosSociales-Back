using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contable.Application
{
    [Table("AppCompromiseLocations")]
    public class CompromiseLocation : Entity
    {
        public Compromise Compromise { get; set; }
        public SocialConflictLocation SocialConflictLocation { get; set; }
    }
}
