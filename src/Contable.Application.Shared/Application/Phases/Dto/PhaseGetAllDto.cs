using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Phases.Dto
{
    public class PhaseGetAllDto : EntityDto
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public List<PhaseMilestoneGetAllDto> Milestones { get; set; }
    }
}
