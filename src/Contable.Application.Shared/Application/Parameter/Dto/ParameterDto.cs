using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Parameters.Dto
{
    public class ParameterDto : EntityDto
    {
        public int Order { get; set; }
        public string Value { get; set; }
        public int ParentId { get; set; }
    }    
}
