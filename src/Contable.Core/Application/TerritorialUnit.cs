using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppTerritorialUnits")]
    public class TerritorialUnit : FullAuditedEntity
    {
        [Column(TypeName = TerritorialUnitConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = TerritorialUnitConsts.FilterType)]
        public string Filter { get; set; }

        public List<TerritorialUnitDepartment> TerritorialUnitDepartments { get; set; }

        public List<TerritorialUnitCoordinator> Coordinators { get; set; }
    }
}
