using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.InterventionPlanRoles.Dto
{
    public class InterventionPlanRoleCreateDto
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public bool ShowDescription { get; set; }
    }
}
