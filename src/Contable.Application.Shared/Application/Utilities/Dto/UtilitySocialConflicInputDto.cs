using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilitySocialConflicInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string SocialConflictCode { get; set; }
        public string SocialConflictDescription { get; set; }
        public int? TerritorialUnitId { get; set; }
        public int? DepartmentId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public ConditionType? Condition { get; set; }
        public int? PersonId { get; set; }
        public int? TypologyId { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
