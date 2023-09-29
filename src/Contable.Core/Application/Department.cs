using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDepartments")]
    public class Department : FullAuditedEntity
    {
        [Column(TypeName = DepartmentConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = DepartmentConsts.EnabledType)]
        public bool Enabled { get; set; }

        [Column(TypeName = DepartmentConsts.CodeType)]
        public string Code { get; set; }

        [Column(TypeName = DepartmentConsts.FilterType)]
        public string Filter { get; set; }

        public List<Province> Provinces { get; set; }

        public List<TerritorialUnitDepartment> TerritorialUnitDepartments { get; set; }

        public List<SocialConflictLocation> Locations { get; set; }
    }
}
