using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppProvinces")]
    public class Province : FullAuditedEntity
    {
        public Department Department { get; set; }

        [Column(TypeName = ProvinceConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = ProvinceConsts.EnabledType)]
        public bool Enabled { get; set; }

        [Column(TypeName = ProvinceConsts.CodeType)]
        public string Code { get; set; }

        [Column(TypeName = ProvinceConsts.FilterType)]
        public string Filter { get; set; }

        public List<District> Districts { get; set; }
    }
}
