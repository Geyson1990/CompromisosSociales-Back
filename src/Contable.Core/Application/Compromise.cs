using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contable.Application
{
    [Table("AppCompromises")]
    public class Compromise : FullAuditedEntity<long>
    {
        [Column(TypeName = CompromiseConsts.CodeType)]
        public string Code { get; set; }

        [Required]
        [Column(TypeName = CompromiseConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = CompromiseConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = CompromiseConsts.TranscriptionType)]
        public string Transcription { get; set; }

        [Column(TypeName = CompromiseConsts.Type)]
        public CompromiseType Type { get; set; }

        [Column(TypeName = CompromiseConsts.FilterType)]
        public string Filter { get; set; }

        [Column(TypeName = CompromiseConsts.CompromiseStateIdType)]
        [ForeignKey("CompromiseState")]
        public int? CompromiseStateId { get; set; }
        public CompromiseState CompromiseState { get; set; }

        [Column(TypeName = CompromiseConsts.CompromiseSubStateIdType)]
        [ForeignKey("CompromiseSubState")]
        public int? CompromiseSubStateId { get; set; }
        public CompromiseSubState CompromiseSubState { get; set; }

        #region Seguimiento    

        public Parameter Status { get; set; }

        [Column(TypeName = CompromiseConsts.IsPriorityType)]
        public bool IsPriority { get; set; }

        [Column(TypeName = CompromiseConsts.PriorityReferenceType)]
        public string PriorityReference { get; set; }

        #endregion

        [ForeignKey("PIPMEF")]
        public long? PIPMEFId { get; set; }
        public PIPMEF PIPMEF { get; set; }

        [Column(TypeName = CompromiseConsts.RecordIdType)]
        [ForeignKey("Record")]
        public long RecordId { get; set; }
        public Record Record { get; set; }

        [ForeignKey("ResponsibleActor")]
        public int? ResponsibleActorId { get; set; }
        public ResponsibleActor ResponsibleActor { get; set; }

        [ForeignKey("ResponsibleSubActor")]
        public int? ResponsibleSubActorId { get; set; }
        public ResponsibleSubActor ResponsibleSubActor { get; set; }

        [Column(TypeName = CompromiseConsts.CompromiseLabelIdType)]
        [ForeignKey("CompromiseLabel")]
        public int? CompromiseLabelId { get; set; }
        public CompromiseLabel CompromiseLabel { get; set; }

        [Column(TypeName = CompromiseConsts.WomanCompromiseType)]
        public bool WomanCompromise { get; set; }

        public List<CompromiseLocation> CompromiseLocations { get; set; }
        public List<CompromiseInvolved> CompromiseInvolveds { get; set; }
        public List<CompromiseResponsible> CompromiseResponsibles { get; set; }
        public List<Situation> Situations { get; set; }
        public List<CompromiseTimeLine> Timelines { get; set; }
        public List<TaskManagement> TaskManagements { get; set; }
        //RQ07 - RF3
        public DateTime? DueDate { get; set; }
        public DateTime? DeadLine { get; set; }
    }
}
