using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.AlertRisks.Dto
{
    public class AlertRiskGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Index";
            }
        }
    }
}