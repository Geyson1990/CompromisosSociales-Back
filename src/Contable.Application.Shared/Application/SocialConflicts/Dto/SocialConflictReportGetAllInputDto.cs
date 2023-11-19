using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictReportGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public string Code { get; set; }
        public int? TerritorialUnitId { get; set; }
        public int? SocialConflictId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool FilterByDate { get; set; }
        public ConflictVerification? Verification { get; set; }
        public ReportType Type { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
