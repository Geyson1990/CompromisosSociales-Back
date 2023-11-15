using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;

namespace Contable.Application.Meets.Dto
{
    public class MeetGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string MeetCode { get; set; }
        public string MeetName { get; set; }
        public SectorMeetSessionType? SectorMeetSessionType { get; set; }
        public int? DepartmentId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? PersonId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
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
