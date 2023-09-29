using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DinamicVariables.Dto
{
    public class DinamicVariableDetailUpdateDto : EntityDto
    {
        public DinamicVariableProvinceDto Province { get; set; }
        public decimal Value { get; set; }
    }
}
