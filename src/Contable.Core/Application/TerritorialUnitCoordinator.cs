using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppTerritorialUnitCoordinators")]
    public class TerritorialUnitCoordinator : Entity
    {
        [Column(TypeName = TerritorialUnitCoordinatorConsts.TerritorialUnitIdType)]
        [ForeignKey("TerritorialUnit")]
        public int TerritorialUnitId { get; set; }
        public TerritorialUnit TerritorialUnit { get; set; }

        [Column(TypeName = TerritorialUnitCoordinatorConsts.PersonIdType)]
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
