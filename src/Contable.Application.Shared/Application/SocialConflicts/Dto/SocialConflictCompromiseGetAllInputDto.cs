using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictCompromiseGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public int? SocialConflictId { get; set; }
        public bool OnlyPriority { get; set; }
        public string Filter { get; set; }
        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Code ASC";
            }
        }
    }
}
