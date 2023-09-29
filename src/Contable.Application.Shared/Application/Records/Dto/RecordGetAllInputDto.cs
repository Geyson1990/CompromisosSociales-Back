using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;

namespace Contable.Application.Records.Dto
{
    public class RecordGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Code { get; set; }
        public string SocialConflictCode { get; set; }
        public int? TerritorialUnitId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Filter { get; set; }
        public bool FilterByDate { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
