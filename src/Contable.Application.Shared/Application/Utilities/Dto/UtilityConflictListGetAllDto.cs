using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilityConflictListGetAllDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string TerritorialUnits { get; set; }
        public ConflictSite Site { get; set; }
        public ConditionType LastCondition { get; set; }
    }
}
