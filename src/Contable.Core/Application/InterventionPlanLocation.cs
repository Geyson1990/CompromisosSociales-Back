using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppInterventionPlanLocations")]
    public class InterventionPlanLocation : FullAuditedEntity
    {
        [Column(TypeName = InterventionPlanLocationConsts.InterventionPlanIdType)]
        [ForeignKey("InterventionPlan")]
        public int InterventionPlanId { get; set; }
        public InterventionPlan InterventionPlan { get; set; }

        [Column(TypeName = InterventionPlanLocationConsts.TerritorialUnitIdType)]
        [ForeignKey("TerritorialUnit")]
        public int TerritorialUnitId { get; set; }
        public TerritorialUnit TerritorialUnit { get; set; }

        [Column(TypeName = InterventionPlanLocationConsts.DepartmentIdType)]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [Column(TypeName = InterventionPlanLocationConsts.ProvinceIdType)]
        [ForeignKey("Province")]
        public int ProvinceId { get; set; }
        public Province Province { get; set; }

        [Column(TypeName = InterventionPlanLocationConsts.DistrictIdType)]
        [ForeignKey("District")]
        public int DistrictId { get; set; }
        public District District { get; set; }

        [Column(TypeName = InterventionPlanLocationConsts.RegionIdType)]
        [ForeignKey("Region")]
        public int? RegionId { get; set; }
        public Region Region { get; set; }

        [Column(TypeName = InterventionPlanLocationConsts.UbicationType)]
        public string Ubication { get; set; }
    }
}
