using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppCompromiseTimeLines")]
    public class CompromiseTimeLine : FullAuditedEntity
    {
        [Column(TypeName = CompromiseTimeLineConsts.CompromiseIdType)]
        [ForeignKey("Compromise")]
        public long CompromiseId { get; set; }
        public Compromise Compromise { get; set; }

        [Column(TypeName = CompromiseTimeLineConsts.PhaseIdType)]
        [ForeignKey("Phase")]
        public int PhaseId { get; set; }
        public Parameter Phase { get; set; }

        [Column(TypeName = CompromiseTimeLineConsts.MilestoneIdType)]
        [ForeignKey("Milestone")]
        public int MilestoneId { get; set; }
        public Parameter Milestone { get; set; }

        [Column(TypeName = CompromiseTimeLineConsts.ProyectedTimeType)]
        public DateTime? ProyectedTime { get; set; }

        [Column(TypeName = CompromiseTimeLineConsts.CompletedTimeType)]
        public DateTime? CompletedTime { get; set; }

        [Column(TypeName = CompromiseTimeLineConsts.ObservationType)]
        public string Observation { get; set; }
    }
}
