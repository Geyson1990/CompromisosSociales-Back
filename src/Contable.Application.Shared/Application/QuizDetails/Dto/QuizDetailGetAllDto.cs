using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.QuizDetails.Dto
{
    public class QuizDetailGetAllDto : EntityDto
    {
        public DateTime CreationTime { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondSurname { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public QuizCompleteType Type { get; set; }
        public QuizDetailStateDto QuizState { get; set; }
        public QuizDetailUserDto Customer { get; set; }
        public QuizDetailUserDto Administrative { get; set; }
    }
}
