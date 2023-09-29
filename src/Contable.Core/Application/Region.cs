using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppRegions")]
    public class Region : FullAuditedEntity
    {
        [Column(TypeName = RegionConsts.DistrictIdType)]
        [ForeignKey("District")]
        public int DistrictId { get; set; }
        public District District { get; set; }

        [Column(TypeName = RegionConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = RegionConsts.CodeType)]
        public string Code { get; set; }

        [Column(TypeName = RegionConsts.UbigeoType)]
        public string Ubigeo { get; set; }

        [Column(TypeName = RegionConsts.EnabledType)]
        public bool Enabled { get; set; }

        [Column(TypeName = RegionConsts.FilterType)]
        public string Filter { get; set; }
    }
}
