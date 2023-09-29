using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProspestiveRisks.Dto
{
    public class ProspectiveRiskStaticVariableOptionDetailGetDto : EntityDto
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public decimal Value { get; set; }
    }
}
