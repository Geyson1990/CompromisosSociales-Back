using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictAlerts.Dto
{
    public class SocialConflictAlertGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string SocialConflictAlertCode { get; set; }
        public string SocialConflictAlertDescription { get; set; }
        public string SocialConflictAlertInformation { get; set; }
        public int? TerritorialUnitId { get; set; }
        public int? DepartmentId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? PersonId { get; set; }
        public int? ResponsibleId { get; set; }
        public int? TypologyId { get; set; }
        public int? RiskId { get; set; }
        public int? SealId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool FilterByDate { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "AlertTime DESC";
            }
        }
    }
}