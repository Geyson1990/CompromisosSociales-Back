using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.QuizDetails.Dto
{
    public class QuizDetailGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public int? StateId { get; set; }
        public QuizCompleteType? CompleteType { get; set; }
        public bool FilterByDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Id ASC";
            }
        }
    }
}
