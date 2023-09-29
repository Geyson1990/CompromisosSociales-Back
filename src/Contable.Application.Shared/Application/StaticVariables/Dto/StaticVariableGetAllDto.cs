using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.StaticVariables.Dto
{
    public class StaticVariableGetAllDto : EntityDto
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public decimal Weight { get; set; }
    }
}
