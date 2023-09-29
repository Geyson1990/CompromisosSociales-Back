using Abp.Application.Services.Dto;
using Contable.Application.Parameters.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Compromises.Dto
{
    public class CompromiseUpdatePIPMEFDto : EntityDto<long>
    {
        public string UnifiedCode { get; set; }
        public string SNIPCode { get; set; }
        public ParameterDto PIPPhase { get; set; }
        public ParameterDto PIPMilestone { get; set; }
    }
}
