using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Milestones.Dto
{
    public class MilestoneGetDto : EntityDto
    {
        public MilestonePhaseGetDto Phase { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
    }
}
