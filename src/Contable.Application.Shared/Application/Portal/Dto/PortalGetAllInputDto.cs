using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Portal.Dto
{
    public class PortalGetAllInputDto
    {
        public int TerritorialUnitId { get; set; }
        public int DepartmentId { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int SocialConflictId { get; set; }
    }
}
