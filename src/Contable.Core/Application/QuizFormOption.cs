using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppQuizFormOptions")]
    public class QuizFormOption : FullAuditedEntity
    {
        [Column(TypeName = QuizFormOptionConsts.QuizFormIdType)]
        [ForeignKey("QuizForm")]
        public int QuizFormId { get; set; }
        public QuizForm QuizForm { get; set; }

        [Column(TypeName = QuizFormOptionConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = QuizFormConsts.IndexType)]
        public int Index { get; set; }

        [Column(TypeName = QuizFormOptionConsts.ExtraType)]
        public bool Extra { get; set; }
    }
}
