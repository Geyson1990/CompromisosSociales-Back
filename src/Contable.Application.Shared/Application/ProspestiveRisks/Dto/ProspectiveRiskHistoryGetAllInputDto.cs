using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProspestiveRisks.Dto
{
    public class ProspectiveRiskHistoryGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public int? ProspectiveRiskHistoryId { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "EvaluatedTime DESC";
            }
        }
    }
}