using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.CrisisCommittees.Dto
{
    public class CrisisCommitteeGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string CaseName { get; set; }
        public string Code { get; set; }
        public string InterventionPlanCode { get; set; }
        public string InterventionPlanCaseName { get; set; }
        public bool FilterByDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Code DESC";
            }
        }
    }
}
