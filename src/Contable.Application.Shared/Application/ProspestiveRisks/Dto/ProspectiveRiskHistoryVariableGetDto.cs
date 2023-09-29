using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProspestiveRisks.Dto
{
    public class ProspectiveRiskHistoryVariableGetDto : EntityDto
    {
        public string Name { get; set; }
        public List<ProspectiveRiskHistoryVariableOptionGetDto> Options { get; set; }
    }
}
