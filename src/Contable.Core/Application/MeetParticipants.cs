using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contable.Application
{
    [Table("AppMeetsParticipants")]
    public class MeetParticipants : FullAuditedEntity
    {
        [Column(TypeName = "INT")]
        [ForeignKey("Meets")]
        public int MeetId { get; set; }
        public Meet Meets { get; set; }

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
