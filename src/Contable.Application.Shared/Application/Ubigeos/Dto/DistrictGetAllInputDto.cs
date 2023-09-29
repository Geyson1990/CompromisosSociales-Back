using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Ubigeos.Dto
{
    public class DistrictGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public int ProvinceId { get; set; }
        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Code";
            }
        }
    }
}