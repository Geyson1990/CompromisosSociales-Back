using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilityDirectoryIndustryGetAllDto : EntityDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Url { get; set; }
        public string Address { get; set; }
        public string AdditionalInformation { get; set; }
        public bool Enabled { get; set; }
        public UtilityDirectoryDistrictDto District { get; set; }
        public UtilityDirectoryIndustrySectorDto DirectorySector { get; set; }
    }
}
