using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDirectoryDialogs")]
    public class DirectoryDialog : FullAuditedEntity
    {
        [Column(TypeName = DirectoryDialogConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = DirectoryDialogConsts.FirstSurnameType)]
        public string FirstSurname { get; set; }

        [Column(TypeName = DirectoryDialogConsts.SecondSurnameType)]
        public string SecondSurname { get; set; }

        [Column(TypeName = DirectoryDialogConsts.JobType)]
        public string Job { get; set; }

        [Column(TypeName = DirectoryDialogConsts.EmailAddressType)]
        public string EmailAddress { get; set; }

        [Column(TypeName = DirectoryDialogConsts.PhoneNumberType)]
        public string PhoneNumber { get; set; }

        [Column(TypeName = DirectoryDialogConsts.MobilePhoneNumberType)]
        public string MobilePhoneNumber { get; set; }

        [Column(TypeName = DirectoryDialogConsts.AdditionalInformationType)]
        public string AdditionalInformation { get; set; }

        [Column(TypeName = DirectoryDialogConsts.EnabledType)]
        public bool Enabled { get; set; }

        [Column(TypeName = DirectoryDialogConsts.DirectoryResponsibleIdType)]
        [ForeignKey("DirectoryResponsible")]
        public int DirectoryResponsibleId { get; set; }
        public DirectoryResponsible DirectoryResponsible { get; set; }

        [Column(TypeName = DirectoryDialogConsts.GovernmentIdType)]
        [ForeignKey("DirectoryGovernment")]
        public int DirectoryGovernmentId { get; set; }
        public DirectoryGovernment DirectoryGovernment { get; set; }
    }
}
