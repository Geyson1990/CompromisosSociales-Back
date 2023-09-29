using Abp.Application.Services.Dto;
using Contable.Application.Parameters.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Compromises.Dto
{
    public class CompromiseTimeLineDto : EntityDto
    {
        public ParameterDto Phase { get; set; }
        public ParameterDto Milestone { get; set; }
        public DateTime? ProyectedTime { get; set; }
        public DateTime? CompletedTime { get; set; }
        public string Observation { get; set; }
        public bool Remove { get; set; }
    }
}
