using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProspestiveRisks.Dto
{
    public class ProspectiveRiskDetailGetDto : EntityDto
    {
        public int StaticVariableOptionId { get; set; }
        public decimal Value { get; set; }
    }
}
