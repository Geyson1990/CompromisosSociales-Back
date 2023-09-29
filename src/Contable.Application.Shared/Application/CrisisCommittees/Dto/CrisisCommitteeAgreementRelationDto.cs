using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.CrisisCommittees.Dto
{
    public class CrisisCommitteeAgreementRelationDto : EntityDto
    {
        public string Description { get; set; }
        public bool Remove { get; set; }
    }
}
