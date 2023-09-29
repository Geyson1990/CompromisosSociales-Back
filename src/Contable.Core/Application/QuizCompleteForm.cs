using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppQuizCompleteForms")]
    public class QuizCompleteForm : FullAuditedEntity
    {
        [Column(TypeName = QuizCompleteFormConsts.QuizCompleteIdType)]
        [ForeignKey("QuizComplete")]
        public int QuizCompleteId { get; set; }
        public QuizComplete QuizComplete { get; set; }

        [Column(TypeName = QuizCompleteFormConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = QuizCompleteFormConsts.IndexType)]
        public int Index { get; set; }

        [Column(TypeName = QuizCompleteFormConsts.RequiredType)]
        public bool Required { get; set; }

        [Column(TypeName = QuizCompleteFormConsts.SelectedOptionIdType)]
        public int SelectedOptionId { get; set; }

        [Column(TypeName = QuizCompleteFormConsts.FormReferenceIdType)]
        public int FormReferenceId { get; set; }

        public List<QuizCompleteOption> Options { get; set; }
    }
}
