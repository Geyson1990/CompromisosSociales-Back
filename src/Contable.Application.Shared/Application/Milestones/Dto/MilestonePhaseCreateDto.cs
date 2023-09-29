using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Milestones.Dto
{
    public class MilestonePhaseCreateDto : EntityDto
    {
        public string Name { get; set; }    
    }
}
