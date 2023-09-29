using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.CrisisCommittees.Dto
{
    public class CrisisCommitteeAlertResponsibleRelationDto : EntityDto
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
