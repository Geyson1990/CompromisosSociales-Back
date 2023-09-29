using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSectorMeetSessionLeaders")]
    public class SectorMeetSessionLeader : FullAuditedEntity
    {
        [Column(TypeName = SectorMeetSessionLeaderConsts.SectorMeetSessionIdType)]
        [ForeignKey("SectorMeetSession")]
        public int SectorMeetSessionId { get; set; }
        public SectorMeetSession SectorMeetSession { get; set; }

        [Column(TypeName = SectorMeetSessionLeaderConsts.Type)]
        public SectorMeetSessionEntityType Type { get; set; }

        [Column(TypeName = SectorMeetSessionLeaderConsts.DirectoryGovernmentIdType)]
        [ForeignKey("DirectoryGovernment")]
        public int? DirectoryGovernmentId { get; set; }
        public DirectoryGovernment DirectoryGovernment { get; set; }

        [Column(TypeName = SectorMeetSessionLeaderConsts.DirectoryIndustryIdType)]
        [ForeignKey("DirectoryIndustry")]
        public int? DirectoryIndustryId { get; set; }
        public DirectoryIndustry DirectoryIndustry { get; set; }

        [Column(TypeName = SectorMeetSessionLeaderConsts.EntityType)]
        public string Entity { get; set; }

        [Column(TypeName = SectorMeetSessionLeaderConsts.RoleType)]
        public string Role { get; set; }

        [Column(TypeName = SectorMeetSessionLeaderConsts.IndexType)]
        public int Index { get; set; }

        public List<SectorMeetSessionTeam> Teams { get; set; }
    }
}
