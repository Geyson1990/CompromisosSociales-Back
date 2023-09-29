using Abp.Configuration;
using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DialogSpaces.Dto
{
    public class DialogSpaceGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Code { get; set; }
        public string CaseName { get; set; }
        public int? DialogSpaceTypeId { get; set; }
        public int? TerritorialUnitId { get; set; }
        public int? DepartmentId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public bool FilterByDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public bool ValidForTerritorialUnit
        {
            get => TerritorialUnitId.HasValue && !DepartmentId.HasValue && !ProvinceId.HasValue && !DistrictId.HasValue;
        }

        public bool ValidForDepartment
        {
            get => DepartmentId.HasValue && !ProvinceId.HasValue && !DistrictId.HasValue;
        }

        public bool ValidForProvince
        {
            get => ProvinceId.HasValue && !DistrictId.HasValue;
        }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
                Sorting = "Code DESC";
        }
    }
}