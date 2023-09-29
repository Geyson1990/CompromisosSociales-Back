using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDirectoryGovernments")]
    public class DirectoryGovernment : FullAuditedEntity
    {
        [Column(TypeName = DirectoryGovernmentConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = DirectoryGovernmentConsts.ShortNameType)]
        public string ShortName { get; set; }

        [Column(TypeName = DirectoryGovernmentConsts.AliasType)]
        public string Alias { get; set; }

        [Column(TypeName = DirectoryGovernmentConsts.AddressType)]
        public string Address { get; set; }

        [Column(TypeName = DirectoryGovernmentConsts.PhoneNumberType)]
        public string PhoneNumber { get; set; }

        [Column(TypeName = DirectoryGovernmentConsts.UrlType)]
        public string Url { get; set; }

        [Column(TypeName = DirectoryGovernmentConsts.AdditionalInformationType)]
        public string AdditionalInformation { get; set; }

        [Column(TypeName = DirectoryGovernmentConsts.EnabledType)]
        public bool Enabled { get; set; }

        [Column(TypeName = DirectoryGovernmentConsts.SectorIdType)]
        [ForeignKey("DirectoryGovernmentSector")]
        public int DirectoryGovernmentSectorId { get; set; }
        public DirectoryGovernmentSector DirectoryGovernmentSector { get; set; }

        [Column(TypeName = DirectoryGovernmentConsts.DistrictIdType)]
        [ForeignKey("District")]
        public int DistrictId { get; set; }
        public District District { get; set; }

        [Column(TypeName = DirectoryGovernmentConsts.DirectoryGovernmentIdType)]
        [ForeignKey("DirectoryGovernmentType")]
        public int? DirectoryGovernmentTypeId { get; set; }
        public DirectoryGovernmentType DirectoryGovernmentType { get; set; }
    }
}
