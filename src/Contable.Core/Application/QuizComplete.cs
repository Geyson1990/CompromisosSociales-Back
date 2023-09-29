using Abp.Domain.Entities.Auditing;
using Contable.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppQuizCompletes")]
    public class QuizComplete : FullAuditedEntity
    {
        [Column(TypeName = QuizCompleteConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = QuizCompleteConsts.SurnameType)]
        public string Surname { get; set; }

        [Column(TypeName = QuizCompleteConsts.SecondSurnameType)]
        public string SecondSurname { get; set; }

        [Column(TypeName = QuizCompleteConsts.EmailAddressType)]
        public string EmailAddress { get; set; }

        [Column(TypeName = QuizCompleteConsts.Type)]
        public QuizCompleteType Type { get; set; }

        [Column(TypeName = QuizCompleteConsts.QuizStateIdType)]
        [ForeignKey("QuizState")]
        public int? QuizStateId { get; set; }
        public QuizState QuizState { get; set; }

        [Column(TypeName = QuizCompleteConsts.AdminitrativeIdType)]
        [ForeignKey("Administrative")]
        public long? AdminitrativeId { get; set; }
        public User Administrative { get; set; }
       
        public List<QuizCompleteForm> Forms { get; set; }
        public List<QuizCompleteResource> Resources { get; set; }
    }
}
