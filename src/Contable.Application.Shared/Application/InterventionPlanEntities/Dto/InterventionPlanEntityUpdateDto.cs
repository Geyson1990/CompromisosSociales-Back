using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.InterventionPlanEntities.Dto
{
    public class InterventionPlanEntityUpdateDto : EntityDto
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public InterventionPlanEntityType Type { get; set; }
    }
}
