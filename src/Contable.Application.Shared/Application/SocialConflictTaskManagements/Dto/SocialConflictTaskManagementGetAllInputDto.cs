using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictTaskManagements.Dto
{
    public class SocialConflictTaskManagementGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public int ConflictId { get; set; }
        public string ConflictCode { get; set; }
        public string ConflictName { get; set; }
        public string TaskTitle { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public ConflictSite ConflictSite { get; set; }
        public ConflictSite ConflictType { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Title ASC";
            }
        }
    }
}
