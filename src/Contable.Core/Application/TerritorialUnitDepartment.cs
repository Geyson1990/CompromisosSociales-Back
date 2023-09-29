using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppTerritorialUnitDepartments")]
    public class TerritorialUnitDepartment : Entity
    {
        [ForeignKey("Department")]
        [Column(TypeName = TerritorialUnitDepartmentConsts.DepartmentIdType)]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [ForeignKey("TerritorialUnit")]
        [Column(TypeName = TerritorialUnitDepartmentConsts.TerritorialUnitIdType)]
        public int TerritorialUnitId { get; set; }
        public TerritorialUnit TerritorialUnit { get; set; }
    }
}
