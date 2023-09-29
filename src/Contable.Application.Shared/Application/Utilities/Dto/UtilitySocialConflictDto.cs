using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilitySocialConflictDto : EntityDto
    {
        public DateTime? LastModificationTime { get; set; }
        public DateTime CreationTime { get; set; }
        public string Code { get; set; }
        public string CaseName { get; set; }
        public string Description { get; set; }
        public string TerritorialUnits { get; set; }
        public string Dialog { get; set; }
        public bool WomanCompromise { get; set; }
        public ConditionType LastCondition { get; set; }
        public UtilityTypologyDto Typology { get; set; }
        public List<UtilitySocialConflictLocationDto> Locations { get; set; }
    }
}
