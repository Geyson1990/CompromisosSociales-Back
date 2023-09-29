using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Phases.Dto
{
    public class PhaseUpdateDto : EntityDto
    {
        public string Name { get; set; }
        public int Index { get; set; }  
    }
}
