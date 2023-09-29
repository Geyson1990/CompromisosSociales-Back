using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilityConflictListGetAllInputDto : PagedInputDto
    {
        public string Code { get; set; }
        public string CaseName { get; set; }
        public ConflictSite Site { get; set; }
        public ConditionType LastCondition { get; set; }
        public int? DepartmentId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
    }
}