using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DirectoryDialogs.Dto
{
    public class DirectoryDialogGovernmentDto : EntityDto
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Alias { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Url { get; set; }
        public string AdditionalInformation { get; set; }
        public bool Enabled { get; set; }
        public DirectoryDialogGovernmentSectorDto DirectoryGovernmentSector { get; set; }
        public DirectoryDialogDistrictDto District { get; set; }
    }
}
