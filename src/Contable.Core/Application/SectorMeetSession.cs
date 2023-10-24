using Abp.Domain.Entities.Auditing;
using Stripe;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSectorMeetSessions")]
    public class SectorMeetSession : FullAuditedEntity
    {
        [Column(TypeName = SectorMeetSessionConsts.SectorMeetIdType)]
        [ForeignKey("SectorMeet")]
        public int SectorMeetId { get; set; }
        public SectorMeet SectorMeet { get; set; }

        [Column(TypeName = SectorMeetSessionConsts.SessionTimeType)]
        public DateTime SessionTime { get; set; }

        [Column(TypeName = SectorMeetSessionConsts.Type)]
        public SectorMeetSessionType Type { get; set; }

        [Column(TypeName = SectorMeetSessionConsts.DepartmentIdType)]
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        [Column(TypeName = SectorMeetSessionConsts.ProvinceIdType)]
        [ForeignKey("Province")]
        public int? ProvinceId { get; set; }
        public Province Province { get; set; }

        [Column(TypeName = SectorMeetSessionConsts.DistrictIdType)]
        [ForeignKey("District")]
        public int? DistrictId { get; set; }
        public District District { get; set; }

        [Column(TypeName = SectorMeetSessionConsts.LocationType)]
        public string Location { get; set; }

        [Column(TypeName = SectorMeetSessionConsts.SideType)]
        public string Side { get; set; }

        [Column(TypeName = SectorMeetSessionConsts.PersonTimeType)]
        public DateTime? PersonTime { get; set; }

        [Column(TypeName = SectorMeetSessionConsts.PersonIdType)]
        [ForeignKey("Person")]
        public int? PersonId { get; set; }
        public Person Person { get; set; }

        public List<SectorMeetSessionAction> Actions { get; set; }
        public List<SectorMeetSessionAgreement> Agreements { get; set; }
        public List<SectorMeetSessionCriticalAspect> CriticalAspects { get; set; }
        public List<SectorMeetSessionSchedule> Schedules { get; set; }
        public List<SectorMeetSessionSummary> Summaries { get; set; }
        public List<SectorMeetSessionResource> Resources { get; set; }
        public List<SectorMeetSessionLeader> Leaders { get; set; }
        public List<SectorMeetSessionFile> ResourcesFiles { get; set; }
    }
}
