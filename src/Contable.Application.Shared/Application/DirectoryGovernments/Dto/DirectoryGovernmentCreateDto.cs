using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DirectoryGovernments.Dto
{
    public class DirectoryGovernmentCreateDto
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Url { get; set; }
        public string AdditionalInformation { get; set; }
        public bool Enabled { get; set; }
        public DirectoryGovernmentSectorRelationDto DirectoryGovernmentSector { get; set; }
        public DirectoryGovernmentDistrictRelationDto District { get; set; }
        public DirectoryGovernmentTypeDto DirectoryGovernmentType { get; set; }
    }
}
