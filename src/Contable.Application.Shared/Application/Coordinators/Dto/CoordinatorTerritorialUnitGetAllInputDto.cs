using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Coordinators.Dto
{
    public class CoordinatorTerritorialUnitGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public int CoordinatorId { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Name DESC";
            }
        }
    }
}