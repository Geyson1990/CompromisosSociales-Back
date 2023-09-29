using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppQuizCompleteFormOptions")]
    public class QuizCompleteOption : FullAuditedEntity
    {
        [Column(TypeName = QuizCompleteFormOptionConsts.QuizCompleteFormIdType)]
        [ForeignKey("QuizCompleteForm")]
        public int QuizCompleteFormId { get; set; }
        public QuizCompleteForm QuizCompleteForm { get; set; }

        [Column(TypeName = QuizCompleteFormOptionConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = QuizCompleteFormOptionConsts.IndexType)]
        public int Index { get; set; }

        [Column(TypeName = QuizCompleteFormOptionConsts.ExtraType)]
        public bool Extra { get; set; }

        [Column(TypeName = QuizCompleteFormOptionConsts.QuizOptionReferenceId)]
        public int QuizOptionReferenceId { get; set; }

        [Column(TypeName = QuizCompleteFormOptionConsts.DescriptionType)]
        public string Description { get; set; }
    }
}
