using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDirectoryIndustries")]
    public class DirectoryIndustry : FullAuditedEntity
    {
        [Column(TypeName = DirectoryIndustryConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = DirectoryIndustryConsts.PhoneNumberType)]
        public string PhoneNumber { get; set; }

        [Column(TypeName = DirectoryIndustryConsts.EmailAddressType)]
        public string EmailAddress { get; set; }

        [Column(TypeName = DirectoryIndustryConsts.UrlType)]
        public string Url { get; set; }

        [Column(TypeName = DirectoryIndustryConsts.AddressType)]
        public string Address { get; set; }

        [Column(TypeName = DirectoryIndustryConsts.AdditionalInformationType)]
        public string AdditionalInformation { get; set; }

        [Column(TypeName = DirectoryIndustryConsts.EnabledType)]
        public bool Enabled { get; set; }

        [Column(TypeName = DirectoryIndustryConsts.DistrictIdType)]
        [ForeignKey("District")]
        public int DistrictId { get; set; }
        public District District { get; set; }

        [Column(TypeName = DirectoryIndustryConsts.DirectorySectorIdType)]
        [ForeignKey("DirectorySector")]
        public int DirectorySectorId { get; set; }
        public DirectorySector DirectorySector { get; set; }
    }
}
