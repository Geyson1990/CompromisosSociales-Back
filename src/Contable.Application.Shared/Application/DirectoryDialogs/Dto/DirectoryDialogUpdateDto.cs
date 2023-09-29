using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DirectoryDialogs.Dto
{
    public class DirectoryDialogUpdateDto : EntityDto
    {
        public string Name { get; set; }
        public string FirstSurname { get; set; }
        public string SecondSurname { get; set; }
        public string Job { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string AdditionalInformation { get; set; }
        public bool Enabled { get; set; }
        public DirectoryDialogResponsibleDto DirectoryResponsible { get; set; }
        public DirectoryDialogGovernmentRelationDto DirectoryGovernment { get; set; }
    }
}
