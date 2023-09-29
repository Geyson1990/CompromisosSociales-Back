using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.InterventionPlans.Dto
{
    public class InterventionPlanGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Code { get; set; }
        public string CaseName { get; set; }
        public int? TerritorialUnitId { get; set; }
        public int? DepartmentId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? PersonId { get; set; }
        public ConflictSite Site { get; set; }
        public bool FilterByDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public bool ValidForTerritorialUnit()
        {
            return TerritorialUnitId.HasValue && !DepartmentId.HasValue && !ProvinceId.HasValue && !DistrictId.HasValue;
        }

        public bool ValidForDepartment()
        {
            return DepartmentId.HasValue && !ProvinceId.HasValue && !DistrictId.HasValue;
        }

        public bool ValidForProvince()
        {
            return ProvinceId.HasValue && !DistrictId.HasValue;
        }
        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Code DESC";
            }
        }
    }
}
