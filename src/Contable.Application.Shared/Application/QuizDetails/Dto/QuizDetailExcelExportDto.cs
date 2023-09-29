using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.QuizDetails.Dto
{
    public class QuizDetailExcelExportDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondSurname { get; set; }
        public string EmailAddress { get; set; }
        public string State { get; set; }
        public QuizCompleteType Type { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string AdministrativeUser { get; set; }
        public string Resources { get; set; }

        public List<QuizDetailQuestionExcelExportDto> Quetions { get; set; }
    }
}
