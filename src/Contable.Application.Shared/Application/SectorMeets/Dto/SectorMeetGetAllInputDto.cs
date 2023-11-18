using Abp.Configuration;
using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Contable.Application.SectorMeets.Dto
{
    public class SectorMeetGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string SectorMeetCode { get; set; }
        public string SectorMeetName { get; set; }
        public SectorMeetSessionType? SectorMeetSessionType { get; set; }
        public int? DepartmentId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? PersonId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool FilterByDate { get; set; }
        public int? State { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
