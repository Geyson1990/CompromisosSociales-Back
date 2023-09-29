using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppQuizStates")]
    public class QuizState : FullAuditedEntity
    {
        [Column(TypeName = QuizStateConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = QuizStateConsts.BackgroundType)]
        public string Background { get; set; }

        [Column(TypeName = QuizStateConsts.ForegroundType)]
        public string Foreground { get; set; }

        [Column(TypeName = QuizStateConsts.EnabledType)]
        public bool Enabled { get; set; }

        [Column(TypeName = QuizStateConsts.DefaultType)]
        public bool Default { get; set; }
    }
}
