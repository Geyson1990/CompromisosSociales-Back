using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.StaticVariables.Dto
{
    public class StaticVariableOptionDetailGetDto : EntityDto
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public decimal Value { get; set; }
    }
}
