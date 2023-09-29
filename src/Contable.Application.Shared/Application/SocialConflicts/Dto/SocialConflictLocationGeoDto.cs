using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictLocationGeoDto
    {
        public SocialConflictTerritorialUnitGeoDto TerritorialUnit { get; set; }
        public SocialConflictDepartmentGeoDto Department { get; set; }
        public SocialConflictProvinceGeoDto Province { get; set; }
        public SocialConflictDistrictGeoDto District { get; set; }
        public SocialConflictRegionGeoDto Region { get; set; }
        public string Ubication { get; set; }
    }
}
