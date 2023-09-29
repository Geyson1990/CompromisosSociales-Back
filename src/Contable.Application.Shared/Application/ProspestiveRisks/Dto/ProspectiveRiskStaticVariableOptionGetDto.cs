using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProspestiveRisks.Dto
{
    public class ProspectiveRiskStaticVariableOptionGetDto : EntityDto
    {
        public int? DinamicVariableId { get; set; }
        public ProspectiveRiskDinamicVariableGetDto DinamicVariable { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public bool Enabled { get; set; }
        public StaticVariableType Type { get; set; }
        public decimal Value { get; set; }
        public List<ProspectiveRiskStaticVariableOptionDetailGetDto> Details { get; set; }
    }
}
