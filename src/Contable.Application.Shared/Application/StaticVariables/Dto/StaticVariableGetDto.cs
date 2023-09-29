using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.StaticVariables.Dto
{
    public class StaticVariableGetDto : EntityDto
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public decimal Weight { get; set; }
        public StaticVariableFamily Family { get; set; }
        public List<StaticVariableOptionGetDto> Options { get; set; }
    }
}
