using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.TaskManagementHistories.Dto
{
    public class TaskManagementHistoryGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Subject { get; set; }
        public string Template { get; set; }
        public string To { get; set; }
        public string Copy { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}