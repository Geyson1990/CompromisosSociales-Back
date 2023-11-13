using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contable.Application
{
    [Table("AppMeets")]
    public class Meet : FullAuditedEntity
    {
        [Column(TypeName = SectorMeetConsts.CodeType)]
        public string Code { get; set; }

        [Column(TypeName = SectorMeetConsts.YearType)]
        public int Year { get; set; }        

        [Column(TypeName = SectorMeetConsts.MeetNameType)]
        public string MeetName { get; set; }        

        [Column(TypeName = SectorMeetConsts.SocialConflictIdType)]
        [ForeignKey("SocialConflict")]
        public int? SocialConflictId { get; set; }
        public SocialConflict SocialConflict { get; set; }

        public int ModalityId { get; set; }

        public int TypeId { get; set; }

        public int PlaceId { get; set; }
        public int LevelRiskId { get; set; }
        public DateTime DayHour { get; set; }

        public string Objet { get; set; }
        public int ResponsibleId { get; set; }
        public string ResponsibleName { get; set; }
        public int RolId { get; set; }
        public string FactorRisk { get; set; }
    }
}
