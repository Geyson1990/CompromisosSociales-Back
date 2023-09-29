using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProspestiveRisks.Dto
{
    public class ProspectiveRiskStaticVariableGetDto : EntityDto
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public List<ProspectiveRiskStaticVariableOptionGetDto> Options { get; set; }
    }
}
