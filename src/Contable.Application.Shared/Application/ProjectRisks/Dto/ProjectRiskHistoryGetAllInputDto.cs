using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectRisks.Dto
{
    public class ProjectRiskHistoryGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public int? ProjectRiskId { get; set; }
        public string Filter { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Stage.Name";
            }
        }
    }
}