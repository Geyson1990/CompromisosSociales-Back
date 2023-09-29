using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.CrisisCommittees.Dto
{
    public class CrisisCommitteeDirectoryGovernmentRelationDto : EntityDto
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Alias { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Url { get; set; }
        public string AdditionalInformation { get; set; }
        public CrisisCommitteeDirectoryGovernmentSectorRelationDto DirectoryGovernmentSector { get; set; }
    }
}
