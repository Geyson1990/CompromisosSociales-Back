using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilityDirectoryGovernmentDto : EntityDto
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Alias { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Url { get; set; }
        public string AdditionalInformation { get; set; }
        public bool Enabled { get; set; }
        public UtilityDirectoryGovernmentSectorDto DirectoryGovernmentSector { get; set; }
        public UtilityDirectoryDistrictDto District { get; set; }
    }
}
