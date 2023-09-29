using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.InterventionPlanActivities.Dto
{
    public class InterventionPlanActivityUpdateDto : EntityDto
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public bool ShowDescription { get; set; }
    }
}
