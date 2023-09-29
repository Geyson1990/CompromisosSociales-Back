using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Compromises.Dto
{
    public class CompromiseGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public int? Type { get; set; }

        public int? TerritorialUnitId { get; set; }

        public bool FilterByDate { get; set; }

        public string Code { get; set; }

        public string CodeRecord { get; set; }

        public string CodeSocialConflict { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string Filter { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
