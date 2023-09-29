using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppQuizForms")]
    public class QuizForm : FullAuditedEntity
    {
        [Column(TypeName = QuizFormConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = QuizFormConsts.Type)]
        public QuizFormType Type { get; set; }

        [Column(TypeName = QuizFormConsts.IndexType)]
        public int Index { get; set; }

        [Column(TypeName = QuizFormConsts.RequiredType)]
        public bool Required { get; set; }

        [Column(TypeName = QuizFormConsts.EnabledType)]
        public bool Enabled { get; set; }

        public List<QuizFormOption> Options { get; set; }
    }
}
