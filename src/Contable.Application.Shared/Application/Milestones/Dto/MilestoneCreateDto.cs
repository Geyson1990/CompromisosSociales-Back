using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Milestones.Dto
{
    public class MilestoneCreateDto
    {
        public MilestonePhaseCreateDto Phase { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
    }
}
