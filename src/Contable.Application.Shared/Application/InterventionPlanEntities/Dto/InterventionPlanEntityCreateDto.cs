using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.InterventionPlanEntities.Dto
{
    public class InterventionPlanEntityCreateDto
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public InterventionPlanEntityType Type { get; set; }
    }
}
