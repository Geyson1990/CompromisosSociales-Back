﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.InterventionPlans.Dto
{
    public class InterventionPlanRiskLevelRelationDto : EntityDto
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
