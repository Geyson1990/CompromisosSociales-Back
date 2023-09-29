using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppRecords")]
    public class Record : FullAuditedEntity<long>
    {
        public SocialConflict SocialConflict { get; set; }

        [Column(TypeName = RecordConsts.CodeType)]
        public string Code { get; set; }

        [Column(TypeName = RecordConsts.TitleType)]
        public string Title { get; set; }

        [Column(TypeName = RecordConsts.DatetimeType)]
        public DateTime? RecordTime { get; set; }

        [Column(TypeName = RecordConsts.FilterType)]
        public string Filter { get; set; }

        public List<RecordResource> Resources { get; set; }

        public List<Compromise> Compromises { get; set; }
    }
}
