using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDistricts")]
    public class District : FullAuditedEntity
    {
        public Province Province { get; set; }

        [Column(TypeName = DistrictConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = DistrictConsts.EnabledType)]
        public bool Enabled { get; set; }

        [Column(TypeName = DistrictConsts.CodeType)]
        public string Code { get; set; }

        [Column(TypeName = DistrictConsts.CodeType)]
        public string Ubigeo { get; set; }

        [Column(TypeName = DistrictConsts.FilterType)]
        public string Filter { get; set; }

        public List<Region> Regions { get; set; }
    }
}
