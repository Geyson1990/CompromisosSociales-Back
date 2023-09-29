using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictAlerts.Dto
{
    public class SocialConflictAlertLocationDto : EntityDto
    {
        public SocialConflictAlertTerritorialUnitDto TerritorialUnit { get; set; }
        public SocialConflictAlertDepartmentLocationDto Department { get; set; }
        public SocialConflictAlertProvinceLocationDto Province { get; set; }
        public SocialConflictAlertDistrictLocationDto District { get; set; }
        public SocialConflictAlertRegionLocationDto Region { get; set; }
        public string Ubication { get; set; }
        public bool Remove { get; set; }
    }
}
