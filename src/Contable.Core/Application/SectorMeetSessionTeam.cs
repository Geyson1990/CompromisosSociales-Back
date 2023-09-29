using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSectorMeetSessionTeams")]
    public class SectorMeetSessionTeam : FullAuditedEntity
    {
        [Column(TypeName = SectorMeetSessionTeamConsts.SectorMeetSessionLeaderIdType)]
        [ForeignKey("SectorMeetSessionLeader")]
        public int SectorMeetSessionLeaderId { get; set; }
        public SectorMeetSessionLeader SectorMeetSessionLeader { get; set; }

        [Column(TypeName = SectorMeetSessionTeamConsts.DocumentType)]
        public string Document { get; set; }

        [Column(TypeName = SectorMeetSessionTeamConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = SectorMeetSessionTeamConsts.SurnameType)]
        public string Surname { get; set; }

        [Column(TypeName = SectorMeetSessionTeamConsts.SecondSurnameType)]
        public string SecondSurname { get; set; }

        [Column(TypeName = SectorMeetSessionTeamConsts.JobType)]
        public string Job { get; set; }

        [Column(TypeName = SectorMeetSessionTeamConsts.EmailAddressType)]
        public string EmailAddress { get; set; }

        [Column(TypeName = SectorMeetSessionTeamConsts.PhoneNumberType)]
        public string PhoneNumber { get; set; }
    }
}
