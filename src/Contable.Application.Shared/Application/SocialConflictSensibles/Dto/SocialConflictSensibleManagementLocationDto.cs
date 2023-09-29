using Abp.Application.Services.Dto;
using Contable.Application.Uploaders.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictSensibles.Dto
{
    public class SocialConflictSensibleManagementLocationDto : EntityDto
    {
        public SocialConflictSensibleManagementDto Management { get; set; }
        public SocialConflictSensiblePersonDto Manager { get; set; }

        public string Description { get; set; }
        public DateTime ManagementTime { get; set; }
        public int? CivilMen { get; set; }
        public int? CivilWomen { get; set; }
        public int? StateMen { get; set; }
        public int? StateWomen { get; set; }
        public int? CompanyMen { get; set; }
        public int? CompanyWomen { get; set; }
        public string VerificationState { get; set; }
        public bool VerificationChange { get; set; }
        public bool VerificationLocation { get; set; }
        public bool Remove { get; set; }

        public List<SocialConflictSensibleManagementResourceDto> Resources { get; set; }
        public List<UploadResourceInputDto> UploadFiles { get; set; }
    }
}
