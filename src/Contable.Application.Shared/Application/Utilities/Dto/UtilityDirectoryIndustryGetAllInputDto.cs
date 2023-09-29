using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilityDirectoryIndustryGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public string Address { get; set; }
        public int? SectorId { get; set; }
        public int? DepartmentId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }

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
                Sorting = "Name ASC";
            }
        }
    }
}
