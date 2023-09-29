using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilityDirectoryGovernmentGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int? DepartmentId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? SectorId { get; set; }
        public int[] SkippedDirectoryGovernmentsIds { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Name ASC";
            }
        }
    }
}