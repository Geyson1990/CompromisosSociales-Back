using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.CrisisCommittees.Dto
{
    public class CrisisCommitteeTaskRelationDto : EntityDto
    {
        public string Description { get; set; }
        public bool Remove { get; set; }
    }
}
