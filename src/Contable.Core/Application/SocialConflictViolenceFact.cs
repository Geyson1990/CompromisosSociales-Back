using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSocialConflictViolenceFacts")]
    public class SocialConflictViolenceFact : FullAuditedEntity
    {
        [Column(TypeName = SocialConflictViolenceFactConsts.SocialConflictIdType)]
        public int SocialConflictId { get; set; }
        public SocialConflict SocialConflict { get; set; }

        [Column(TypeName = SocialConflictViolenceFactConsts.ManagerIdType)]
        public int? ManagerId { get; set; }
        public Person Manager { get; set; }

        [Column(TypeName = SocialConflictViolenceFactConsts.FactIdType)]
        public int FactId { get; set; }
        public Fact Fact { get; set; }

        [Column(TypeName = SocialConflictViolenceFactConsts.StartTimeType)]
        public DateTime StartTime { get; set; }

        [Column(TypeName = SocialConflictViolenceFactConsts.EndTimeType)]
        public DateTime? EndTime { get; set; }

        [Column(TypeName = SocialConflictViolenceFactConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = SocialConflictViolenceFactConsts.ResponsibleType)]
        public string Responsible { get; set; }

        [Column(TypeName = SocialConflictViolenceFactConsts.ActionsType)]
        public string Actions { get; set; }

        [Column(TypeName = SocialConflictViolenceFactConsts.InjuredMenType)]
        public int InjuredMen { get; set; }

        [Column(TypeName = SocialConflictViolenceFactConsts.InjuredWomenType)]
        public int InjuredWomen { get; set; }

        [Column(TypeName = SocialConflictViolenceFactConsts.DeadMenType)]
        public int DeadMen { get; set; }

        [Column(TypeName = SocialConflictViolenceFactConsts.DeadWomenType)]
        public int DeadWomen { get; set; }

        public List<SocialConflictViolenceFactLocation> Locations { get; set; }
    }
}
